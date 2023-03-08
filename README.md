# KafkaLocalPlauground
A playground with local Kafka setup, several .net apps and azure functions

* Run Kafka locally
```
docker compose up -d
```

* Create a topic
```
docker compose exec broker \
  kafka-topics --create \
    --topic purchases \
    --bootstrap-server localhost:9092 \
    --replication-factor 1 \
    --partitions 1
```


* Start `producer` to produce events to Kafka topic
* Start `consumer` to read events from Kafka topic
* Start `FunctionApp.Consumer` to read Kafka topic from Azure Function
* Start `FunctionApp.Output` to produce events tp Kafka topic as an output binding from Azure Function. A trigger for the function is get query `http://localhost:6444/api/Function1?message=HelloFromWeb`