import { z } from "zod";

const noteDtoSchema = z.object({
  id: z.string().uuid(),
  name: z.string().nonempty("name is required").max(100, "Name must be below 100 characters"),
  text: z.string().nonempty("text is required").max(300, "Text must be below 300 characters")
});

const createNoteDtoSchema = z.object({
  name: z.string().nonempty("name is required").max(100, "Name must be below 100 characters"),
  text: z.string().nonempty("text is required").max(300, "Text must be below 300 characters")
});

const updateNoteDtoSchema = z.object({
  name: z.string().nonempty("name is required").max(100, "Name must be below 100 characters"),
  text: z.string().nonempty("text is required").max(300, "Text must be below 300 characters")
});

export type NoteDto = z.infer<typeof noteDtoSchema>;
export type CreateNoteDto = z.infer<typeof createNoteDtoSchema>;
export type UpdateNoteDto = z.infer<typeof updateNoteDtoSchema>;

export const noteSchemas = { noteDtoSchema, createNoteDtoSchema, updateNoteDtoSchema };
