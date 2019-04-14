# IdentityGatewayDemo
Ocelot + IdentityServer4 to build microservice gateway based on .NET Core plateform

Architecture picture:    
![Demo Architecture](https://github.com/dotNetXA/IdentityGatewayDemo/blob/master/architecture.jpg "Optional title")

There are two ways to config this demo code in your local environment:    
1.In Memory mode   
  In this model you just need to mock all data in memory and identityserver4 and ocelot will load configuration from it, for ocelot by default if you don't config the database in you ConfigurationService ,it will load gateway config from ocelot.json file:
    
    private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://0.0.0.0:8081")
                .ConfigureAppConfiguration((host,builder) => {
                 builder.SetBasePath(host.HostingEnvironment.ContentRootPath)
                .AddJsonFile("ocelot.json"); })
                .UseStartup<Startup>();
   
  
  and the ocelot samlpe config file will looks like:
      
      {
	  "ReRoutes": [ //路由是API网关最基本也是最核心的功能、ReRoutes下就是由多个路由节点组成。
	    {
	      "DownstreamPathTemplate": "", //下游服务模板
	      "UpstreamPathTemplate": "", //上游服务模板
	      "UpstreamHttpMethod": [ "Get" ],//上游方法类型Get,Post,Put
	      "AddHeadersToRequest": {},//需要在转发过程中添加到Header的内容
	      "FileCacheOptions": { //可以对下游请求结果进行缓存，主要依赖于CacheManager实现
	        "TtlSeconds": 10,
	        "Region": ""
	      },
	      "ReRouteIsCaseSensitive": false,//重写路由是否区分大小写
	      "ServiceName": "",//服务名称
	      "DownstreamScheme": "http",//下游服务schema：http, https
	      "DownstreamHostAndPorts": [ //下游服务端口号和地址
	        {
	          "Host": "localhost",
	          "Port": 8001
	        }
	      ],
	      "RateLimitOptions": { //限流设置
	        "ClientWhitelist": [], //客户端白名单
	        "EnableRateLimiting": true,//是否启用限流设置
	        "Period": "1s", //每次请求时间间隔
	        "PeriodTimespan": 15,//恢复的时间间隔
	        "Limit": 1 //请求数量
	      }，
	      "QoSOptions": { //服务质量与熔断,熔断的意思是停止将请求转发到下游服务。当下游服务已经出现故障的时候再请求也是无功而返，
	      并且增加下游服务器和API网关的负担，这个功能是用的Polly来实现的，我们只需要为路由做一些简单配置即可
	        "ExceptionsAllowedBeforeBreaking": 0, //允许多少个异常请求
	        "DurationOfBreak": 0, //熔断的时间，单位为秒
	        "TimeoutValue": 0 //如果下游请求的处理时间超过多少则自如将请求设置为超时
	      }
	    }
	  ],
	  "UseServiceDiscovery": false,//是否启用服务发现
	  "Aggregates": [ //请求聚合
	    {
	      "ReRouteKeys": [ //设置需要聚合的路由key
	        "booking",
	        "passenger"
	      ],
	      "UpstreamPathTemplate": "/api/getbookingpassengerinfo" //暴露给外部的聚合请求路径
	    },
	  "GlobalConfiguration": { //全局配置节点
	    "BaseUrl": "https://localhost:5000" //网关基地址
	  }
	} 
     
 for more detail configuration you can find it in official document:
 [Ocelot](https://ocelot.readthedocs.io/en/latest/introduction/gettingstarted.html) .    
  for identityservice part you only need to change some code in you startup.cs file:
  
     public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // 1.config data in memory
            //ConfigService.ConfigServiceInMemory(services);

            //2.config data in database
            ConfigServiceInSqlServer(services);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        } 
    
2.DataBase mode    
for database mode, firstly you need to init db schema for ocelot and identityserver4,we build sqlservice instance based on docker container and this is defined in docker-compose.yml file in root directory.
    
    version: '3.4'
	services:
	  mssqlserver:
	    image: microsoft/mssql-server-linux:2017-CU9-GDR2
	    hostname: mssqlserver
	    environment:
	      "ACCEPT_EULA": "Y"
	      "SA_PASSWORD": "Password123"
	    ports:
	      - "1450:1433"
	    volumes:
	      - sqldata:/var/opt/mssqlserver
	    networks:
	      - private
	
	  gateway1:
	    image: gatewayservice1
	    ports:
	      - "8081:8081"
	    build:
	      context: GatewayService
	      dockerfile: Dockerfile
	    networks:
	      - public
	      - private
	
	  gateway2:
	    image: gatewayservice2
	    ports:
	      - "8082:8081"
	    build:
	      context: GatewayService
	      dockerfile: Dockerfile
	    networks:
	      - public
	      - private
	
	  identity:
	    image: identityserver
	    ports:
	      - "5008:5008"
	    build:
	      context: IdentityServer
	      dockerfile: Dockerfile
	    networks:
	      - private
	
	  orderservice:
	    image: orderservice
	    ports:
	      - "5001:5001"
	    build:
	      context: OrderService
	      dockerfile: Dockerfile
	    networks:
	      - private
	
	  productservice:
	    image: productservice
	    ports:
	      - "5002:5002"
	    build:
	      context: ProductService
	      dockerfile: Dockerfile
	    networks:
	      - private
	
	  nginx:
	    build: ./nginx
	    container_name: nginx_server
	    ports:
	      - "8083:8083"
	
	volumes:
	  sqldata:
	
	networks:
	  public:
	  private:

for ocelot we defined db schema in our code, you can generate database shcema by EntityFramewrok Code, in your GatewayService project directory execute these command to do migrate:
    
    dotnet ef migrations add [Migration name]    
    dotnet ef database update
after schema migrate successfully we need to init config data to db by these script,please notice that the Host should be your local machine ip address
    
     --插入全局测试信息
	 insert into GlobalConfigurations(GatewayName,RequestIdKey,IsDefault,InfoStatus)
	 values('测试网关','test_gateway',1,1);
	
	--插入路由测试信息 
	 insert into ReRoutes values(1,'/api/getproduct','[ "GET" ]','','http','/product','[{"Host": "192.168.191.3","Port": 5002 }]',
	 '{"AuthenticationProviderKey": "ProductIdentityKey","AllowScopes": [ ]}','','','','','','',0,1,'');
	
	 insert into ReRoutes values(1,'/api/getorder','[ "GET" ]','','http','/api/order','[{"Host": "192.168.191.3","Port": 5001 }]',
	 '{"AuthenticationProviderKey": "OrderIdentityKey","AllowScopes": [ ]}','','','','','','',0,1,'');

for identityserver4 official already publish nuget package named IdentitySeever4.EntityFramework.Storage, you can install this package to you project and then do db migrate to generate schema by these command:
    
    dotnet ef migrations add [Migration name] -c PersistedGrantDbContext -o Data/Migrations/IdentityServer/PersistedGrantDb

	dotnet ef database update --context PersistedGrantDbContext
	
	
	dotnet ef migrations add [Migration name] -c ConfigurationDbContext -o Data/Migrations/IdentityServer/ConfigurationDb
	
	dotnet ef database update --context ConfigurationDbContext

we added test data in code, when first tome application start up these data will init to database, we don't need to init it manually:
    
    public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
            };
        }

        // scopes define the API resources in your system
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("productapi", "this is product api"),
                new ApiResource("orderapi", "this is order api")
            };
        }

        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "product",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    ClientSecrets =
                    {
                        new Secret("productsecret".Sha256())
                    },

                    AllowedScopes = new [] { "productapi",
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile }
                },
                new Client
                {
                    ClientId = "order",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    ClientSecrets =
                    {
                        new Secret("ordersecret".Sha256())
                    },

                    AllowedScopes = new [] { "orderapi",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile }
                }
            };
        }
  before you start application you need to change db connectionstring in appsetting.json:
      
      "DefaultConnection": "Server=192.168.191.3,1450;Database=GatewayServer;User Id=sa;Password=Password123;MultipleActiveResultSets=True;"
we write down some start script in go file in solution directory,when you first time to run this project you need to build docker image, so you can use ./go start command to start whole application, after that you can use command ./go up to start the instance which already built.
     
 