# ChainOfResponsibility.NET

This project is to showcase "chain of responsibility" pattern with the least possible noise.

The goal of CoR is to form a _pipeline_ consisting of one or mode _nodes_.
Each node have single, focused _responsibility_.

A _pipeline_ structure resembles a linked list/chain; one _node_ will have a reference to netxt _node_.
When the first _node_ is invoked, it will do it's _responsibility_ and then call the next node.

