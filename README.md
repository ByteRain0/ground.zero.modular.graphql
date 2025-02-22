# Ground Zero - Modular Monolith

Ground Zero is a pet project aimed at exploring a GraphQL-based tech stack in combination with ASPIRE while building a modular monolith solution. <br> 
This repository serves as a playground for testing architectural decisions, tooling, and best practices in modular monolith development.

## 📌 Project Goals
- Experiment with GraphQL as the core API layer.
- Design and implement a modular monolith architecture.
- Document architectural decisions and trade-offs using ADRs.
- Generate architecture diagrams from code using Structurizr.
- Provide debugging and development guidance.

## 📂 Project Structure
```
/ground-zero
│── /.config        # Configurations for developer tools
│── /configurations # Configurations used by infrastructure components
│── /docs           # Documentation, ADRs, and architecture diagrams
│── /src            # Application source code
│── /tests          # Automated tests
│── README.md       # This file
│── ...             # Other utilities
```

## 🛠️ Additional tools
- **Structurizr**: Generates architecture diagrams from code.
- **ADR Tools**: Captures and manages architectural decisions.
- Other technologies will be detailed within the ADRs.

## 📜 Architectural Decisions
We use [ADR Tools](https://github.com/npryce/adr-tools) to document architectural decisions. These are integrated with Structurizr for visualization. You can find ADRs in the `/docs/architecture/decisions` directory.

## 📊 Architecture Diagrams
Architecture diagrams are auto-generated using Structurizr and can be found in `/docs/`. These diagrams provide a visual representation of the system’s structure and dependencies.

## 🐞 Debugging Guide
A step-by-step guide on how to debug the solution is available inside the ADR documentation. Refer to the `/docs` for troubleshooting and debugging guides.

## 📌 Contributing
This project is primarily for exploration and testing. Contributions are welcome but may not be actively maintained.

---
_Developed as an experimental playground for GraphQL-based modular monolith architecture._

