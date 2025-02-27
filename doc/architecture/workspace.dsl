workspace "Name" "Description" {

    !adrs decisions
    !identifiers hierarchical

    model {
        software_system = softwareSystem "Japanese cultural app" {
            aspire_dashboard = container "Aspire Dashboard" "Management dashboard used for local development" {
                tags "AspireBase"
            }
            telemetry_collector = container "Telemetry Collector" {
                -> aspire_dashboard "Push telemetry"
            }
            japanese_database = container "Manga.DB" {
                tags "Database"
            }
            rating_database = container "Rating.Cache" {
                tags "Database"
            }
            keycloak = container "Keycloak" {
                tags "Service"
            }
            migration_service = container "Japanese.Api.MigrationService" "Service that can be run from the pipeline in order to apply migrations to the Database" {
                -> japanese_database
                -> telemetry_collector
            }
            message_broker = container "RabbitMq" {
                tags "Messagebroker"
            }
            manga_api = container "Japanese.Api" {
                tags "Service"
                -> japanese_database
                -> telemetry_collector
                -> keycloak "Auth" "HTTP"
                -> message_broker "Pub/Sub"
                
                group "Utilities" {
                    mediatr = component "MediatR"
                    Otel = component "OTel"
                    auth = component "Auth"
                    error_filters = component "ErrorFilters"
                    hangfire = component "Hangfire"
                    masstransit = component "Masstransit"
                }
                group "Anime module" {
                    graphql_topic_event_sender = component "GraphQL topic event sender"
                    anime_model = component "Anime" "Domain model"
                    anime_configuration = component "Anime DB Configuration" "Configure how the Anime model maps to the database table" {
                        -> anime_model "Configure"
                    }
                    anime_data_context = component "Anime DbContext" "DbContext that maps to the Anime Schema" {
                        -> japanese_database
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
                        -> anime_data_context "Query data"
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
                    manga_model = component "Manga" "Domain model"
                    author_model = component "Author" "Domain model"
                    manga_configuration = component "Manga Configuration" "Configure how the Manga model maps to the database table"{
                        -> manga_model "Configure"
                    }
                    author_configuration = component "Author Configuration" "Configure how the Author model maps to the database table" {
                        -> author_model "Configure"
                    }
                    author_settings_model = component "Author Settings Module" "Configure dynamic settings field for graphql node and map to model property" {
                        -> author_model "Configure"
                    }
                    manga_data_context = component "Manga DbContext" "DbContext that maps to the Manga Schema" {
                        -> japanese_database
                        -> manga_configuration "Use configuration"
                        -> author_configuration "Use configuration"
                    }
                    manga_query_handler = component "Manga Query Handler" "Return IQueryable and allow GraphQL execution engine to directly tap into the EfCore DbContext to build queries and projections" {
                        -> manga_data_context "Query data"
                    }
                    manga_queries = component "Manga Queries" {
                        -> manga_query_handler "Send query" "MediatR"
                    }
                }
                graphQL_execution_engine = component "GraphQL engine" {
                    -> anime_mutations
                    -> anime_queries
                    -> anime_subscriptions
                    -> manga_queries
                    -> author_settings_model "Load configuration"
                }
                fairybread_validator = component "FairyBread" "Validate GraphQL requests before they hit MediatR" {
                    -> graphQL_execution_engine "Register validators"
                }
                host = component "Host" {
                    -> graphQL_execution_engine "Host"
                }
            }
            rating_api = container "Rating.API" {
                tags "Service"
                -> rating_database
                -> telemetry_collector
                -> keycloak "Auth" "HTTP"
                -> message_broker "Pub/Sub"
            }
            api_gateway = container "Gateway" "Fusion gateway" {
                -> rating_api "Query for ratings" "GraphQL"
                -> manga_api "Query for anime/manga information" "GraphQL"
            }
            aspire_host = container "App.Host" "Aspire app host used for local development" {
                tags "AspireBase"
            }
            web_ui = container "Web Application" {
                tags "WebBrowser
                -> api_gateway "Send requests" "HTTP"
                -> keycloak "Authenticate" "HTTP"
            }
        }
        user = person "Client" {
            -> software_system.web_ui "Interact" "HTTP"
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