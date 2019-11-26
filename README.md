# ChainOfResponsibility.NET

This project is to showcase "chain of responsibility" pattern with the least possible noise.

The goal of CoR is to form a _pipeline_ consisting of one or mode _nodes_.
Each node have single, focused _responsibility_.

A _pipeline_ structure resembles a linked list/chain; one _node_ will have a reference to next _node_.
When a _node_ is invoked, it will have to do it's own _responsibility_ as well as invoking the next node at the right point of execution.

BasicPipeline.csproj demonstrate a basic pipeline.
ThreadSpawningPipeline.csproj demonstrate a similar pipeline, but with the addition of spawning asynchronous Thread in the middle of the pipeline lifecycle. This async Thread need to be cancelled just before the pipeline lifecycle ends. 
