#!/usr/bin/env sh

if [ $# = 0 ]; then
  echo "
  Usage: ./go [commands]
  
  Commands:
    up                         Start all services
    stop                       Stop and remove services
    restart                    Stop and remove all services then start them all
    start [name]                 Build and start all or one services

  Examples: 
    ./go start"

fi


if [ "$1" = "start" ]; then
  docker-compose rm -f -s $2
  docker-compose -f docker-compose.yml up --build -d $2
fi

if [ "$1" = "stop" ]; then
  docker-compose down
fi

if [ "$1" = "restart" ]; then
  docker-compose -f docker-compose.yml -f restart
fi

if [ "$1" = "up" -o "$2" = "up" ]; then
  docker-compose -f docker-compose.yml -f up -d
fi
