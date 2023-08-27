import { CreateNoteDto, NoteDto, UpdateNoteDto } from "~/schemas/note.schemas";
import { httpClient } from "~/utils/http-client";

const get = async (args: { id: string }) => {
  const result = await httpClient.get<NoteDto>(`notes/${args.id}`);
  return result.data;
};

const add = async (args: { body: CreateNoteDto }) => {
  const result = await httpClient.post<NoteDto>("notes", args.body);
  return result.data;
};

const remove = async (args: { id: string }) => {
  const result = await httpClient.delete<NoteDto>(`notes/${args.id}`);
  return result.data;
};

const update = async (args: { body: UpdateNoteDto; id: string }) => {
  const result = await httpClient.put<NoteDto>(`notes/${args.id}`, args.body);
  return result.data;
};

const getAll = async () => {
  const result = await httpClient.get<NoteDto>("notes");
  return result.data;
};

export const noteServices = { get, add, update, remove, getAll };
