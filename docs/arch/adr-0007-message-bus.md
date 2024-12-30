# TODO: rephrase a bit.
# 1. Record Architecture Decisions

**Date:** 2023-04-15

## Problem

Our project is still in its early stages and currently operates as a single deployment unit. </br>
We need a solution that enables components to communicate and interact without being tightly coupled, </br>
while maintaining high levels of maintainability. </br>
Our priority is to focus on business processes and interactions rather than investing time in selecting an out-of-process message broker at this stage. </br>
This decision can be deferred to a later point in the project.

## Decision
