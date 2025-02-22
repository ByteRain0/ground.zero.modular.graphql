workspace "Name" "Description" {

    !identifiers hierarchical

    model {
        software_system = softwareSystem "Japanese cultural app" {
            telemetry_collector = container "Telemetry Collector"
            japanese_database = container "Manga.DB" {
                tags "Database"
            }
            rating_database = container "Rating.Cache" {
                tags "Database"
            }
            keycloak = container "Keycloak" {
                tags "Service"
            }
            migration_service = container "Japanese.Api.MigrationService" {
                -> japanese_database "Apply migrations"
                -> telemetry_collector "Send telemetry" "gRPC"
            }
            message_broker = container "RabbitMq" {
                tags "Messagebroker"
            }
            manga_api = container "Japanese.Api" {
                tags "Service"
                -> japanese_database
                -> telemetry_collector "Send telemetry" "gRPC"
                -> keycloak "Auth" "HTTP"
                -> message_broker "Pub/Sub"
                
                group "Utilities" {
                    core = component "Core"
                    common = component "Common"
                    graphql_topic_event_sender = component "GraphQL topic event sender"
                    graphql_validation_filter = component "GraphQL business validation filter"
                    mediatR_activity_tracing_behavior = component "MediatR activity tracing behavior" "Create traces for request handlers"
                    mediatR_logging_behavior = component "MediatR logging behavior" "Create logs for request handlers"
                    mediatR_performance_behavior = component "MediatR performance behavior" "Monitor request handler performance"
                    mediatR_authorization_behavior = component "MediatR auth behavior" "Validate user is authenticated and authorized"
                    mediatR = component "MediatR" {
                        -> mediatR_activity_tracing_behavior "Register"
                        -> mediatR_logging_behavior "Register"
                        -> mediatR_performance_behavior "Register"
                        -> mediatR_authorization_behavior "Register"
                    }
                }
                group "Anime module" {
                    anime_model = component "Anime" "Domain model"
                    anime_configuration = component "Anime DB Configuration" "Configure how the Anime model maps to the database table" {
                        -> anime_model "Configure"
                    }
                    anime_data_context = component "Anime DbContext" "DbContext that maps to the Anime Schema" {
                        -> japanese_database "Interact with database"
                        -> anime_configuration "Use configuration"
                    }
                    anime_messaging_notification_handler = component "Messaging Notification Handler" {
                        -> message_broker "Publish notification" "Masstransit"
                    }
                    anime_graphql_notification_handler = component "GraphQL Notification Handler" {
                        -> graphql_topic_event_sender "Publish event"
                    }
                    anime_command_handlers = component "Anime command handlers" {
                        -> anime_data_context "Mutate data"
                        -> anime_messaging_notification_handler "Publish notification" "MediatR"
                        -> anime_graphql_notification_handler "Publish notification" "MediatR"
                    }
                    anime_data_loader = component "Anime DataLoaders" {
                        -> anime_data_context "Batch query data"
                    }
                    anime_query_handlers = component "Anime query handlers" {
                        -> anime_data_loader "Query data"
                    }
                    anime_node = component "Anime node" "Configure how Anime model is exposed via GraphQL" {
                        -> anime_model "Configure"
                    }
                    anime_mutations = component "Anime mutations" {
                        -> anime_command_handlers "Send command" "MediatR"
                    }
                    anime_queries = component "Anime queries" {
                        -> anime_query_handlers "Send query" "MediatR"
                        -> anime_node "Expose for quering"
                    }
                    anime_subscriptions = component "Anime subscriptions" {
                        -> graphql_topic_event_sender "Subscribe to anime events"
                    }
                }
                
                group "Manga module" {
                    manga_contracts = component "Manga.Contracts" {
                        -> core
                        -> common
                    }
                    manga_service = component "Manga.Service" {
                        -> manga_contracts "Implement"
                        -> japanese_database
                    }
                    manga_graphql = component "Manga graphQL" {
                        -> manga_contracts "Expose"
                    }
                }
                
                graphQL_execution_engine = component "GraphQL engine" {
                    -> anime_mutations
                    -> anime_queries
                    -> anime_subscriptions
                    -> graphql_validation_filter
                }

                fairybread_validator = component "FairyBread" "Validate GraphQL requests before they hit MediatR" {
                    -> graphQL_execution_engine "Register validators"
                }
                
                host = component "Host" {
                    -> manga_graphql
                    -> manga_service
                    -> graphQL_execution_engine
                }
            }
            rating_api = container "Rating.API" {
                tags "Service"
                -> rating_database
                -> telemetry_collector "Send telemetry" "gRPC"
                -> keycloak "Auth" "HTTP"
                -> message_broker "Pub/Sub"
            }
            api_gateway = container "Gateway" {
                -> rating_api "Query for ratings" "GraphQL"
                -> manga_api "Query for anime/manga information" "GraphQL"
            }
            aspire_host = container "App.Host" {
                tags "AspireBase"
            }
        }
        user = person "Client" {
            tags "WebBrowser"
            -> software_system.api_gateway "Interact with" "HTTP"
            -> software_system.keycloak "Authenticate" "HTTP"
        }

    }

    views {
        systemContext software_system "Diagram1" {
            include *
            autolayout lr
        }

        container software_system "japanese_culture_system" {
            include *
            autolayout lr
        }
        component software_system.manga_api "manga_api_component" {
            include *
            autolayout lr
        }



        styles {
            element "Element" {
                color #ffffff
            }
            element "Person" {
                background #048c04
                shape person
            }
            element "Software System" {
                background #047804
            }
            element "Container" {
                background #55aa55
            }
            element "Component" {
                background #55aa55
            }
            element "AspireBase" {
                background #7242f5
            }
            element "Database" {
                shape cylinder
            }
            element "Messagebroker" {
                shape Pipe
            }
            element "WebBrowser" {
                shape Window
            }
            element "MobileApp" {
                shape MobileDevicePortrait
            }
            element "Service" {
                shape hexagon
            }
            element "FileStorage" {
                shape folder
            }
            element "ExternalActor" {
                background #999999
            }
            element "ExternalSystem" {
                background #999999
            }
            element "ObservabilityBE" {
                background #ebc934
            }
        }
    }

    configuration {
        scope softwaresystem
    }

}