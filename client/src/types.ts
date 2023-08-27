import { ProblemDetails } from "./utils/problem-details";

declare module "@tanstack/react-query" {
  interface Register {
    defaultError: ProblemDetails;
  }
}
