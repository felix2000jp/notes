import { Card, Group, Text } from "@mantine/core";
import { NoteDto } from "~/schemas/note-schemas";
import { RemoveNoteModal } from "./remove-note-modal";
import { UpdateNoteModal } from "./update-note-modal";

type NoteCard = { note: NoteDto };

export const Note = ({ note }: NoteCard) => {
  return (
    <Card shadow="lg" padding="lg" radius="lg" withBorder>
      <Text size="lg" weight={600} color="dark">
        {note.name}
      </Text>

      <Text size="md" weight={400} color="dimmed">
        {note.text}
      </Text>

      <Group mt="md">
        <UpdateNoteModal note={note} />
        <RemoveNoteModal noteId={note.id} />
      </Group>
    </Card>
  );
};
