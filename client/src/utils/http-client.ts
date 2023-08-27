import axios from "axios";
import { handleError } from "./problem-details";

export const httpClient = axios.create({
  baseURL: "http://localhost:8080/api/",
  withCredentials: true
});

httpClient.interceptors.response.use(
  (reply) => reply,
  (error) => handleError(error)
);
