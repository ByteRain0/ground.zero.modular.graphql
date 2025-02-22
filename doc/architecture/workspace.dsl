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
                }
                group "Anime module" {
                    anime_contracts = component "Anime.Contracts" {
                        -> core
                        -> common
                    }
                    anime_service = component "Anime.Service" {
                        -> anime_contracts "Implement"
                    }
                    anime_graphql = component "Anime.GraphQL" {
                        -> anime_contracts "Expose"
                        -> japanese_database
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
                    manga_graphql = component "Manga.GraphQL" {
                        -> manga_contracts "Expose"
                    }
                }
                graphQL_execution_engine = component "GraphQL engine" {
                    -> manga_graphql "Query data"
                    -> anime_graphql "Query data"
                }
                host = component "Host" {
                    -> manga_graphql
                    -> manga_service
                    -> anime_graphql
                    -> anime_service
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