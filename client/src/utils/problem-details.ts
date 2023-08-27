import { type AxiosError } from "axios";
import { z } from "zod";

const problemDetailsSchema = z.object({
  type: z.string().url("Error type should be an url"),
  title: z.string().nonempty("Error title is required"),
  status: z.number().min(400, "Error code cannot be below 400").max(599, "error code cannot be above 599"),
  detail: z.string().nonempty("Error detail is required")
});

export const handleError = (error: unknown) => {
  const axiosError = error as AxiosError;
  const parsed = problemDetailsSchema.safeParse(axiosError.response?.data);

  if (parsed.success) throw parsed.data;
  throw {
    type: "https://tools.ietf.org/html/rfc7231#section-6.6.1",
    title: "Oops, this was not supposed to happen 😅",
    status: 500,
    detail: "I cannot be sure what went wrong, but the server is currently down"
  };
};

export type ProblemDetails = z.infer<typeof problemDetailsSchema>;
