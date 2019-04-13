-- --插入全局测试信息
-- insert into GlobalConfigurations(GatewayName,RequestIdKey,IsDefault,InfoStatus)
-- values('测试网关','test_gateway',1,1);

--插入路由测试信息 
-- insert into ReRoutes values(1,'/api/getproduct','[ "GET" ]','','http','/product','[{"Host": "192.168.191.3","Port": 5002 }]',
-- '{"AuthenticationProviderKey": "ProductIdentityKey","AllowScopes": [ ]}','','','','','','',0,1,'');

-- insert into ReRoutes values(1,'/api/getorder','[ "GET" ]','','http','/api/order','[{"Host": "192.168.191.3","Port": 5001 }]',
-- '{"AuthenticationProviderKey": "OrderIdentityKey","AllowScopes": [ ]}','','','','','','',0,1,'');

select * from GlobalConfigurations
select * from ReRoutes

