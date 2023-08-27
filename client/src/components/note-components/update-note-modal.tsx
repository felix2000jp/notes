import { Button, Group, Modal, TextInput, Textarea } from "@mantine/core";
import { useForm, zodResolver } from "@mantine/form";
import { useDisclosure } from "@mantine/hooks";
import { notifications } from "@mantine/notifications";
import { IconAlertSmall } from "@tabler/icons-react";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { NoteDto, UpdateNoteDto, noteSchemas } from "~/schemas/note-schemas";
import { noteServices } from "~/services/note-services";

type UpdateNoteModal = { note: NoteDto };

export const UpdateNoteModal = ({ note }: UpdateNoteModal) => {
  const queryClient = useQueryClient();
  const [opened, { open, close }] = useDisclosure(false);

  const updateNote = useMutation({
    mutationKey: ["update"],
    mutationFn: noteServices.update,
    onSuccess: () => queryClient.invalidateQueries({ queryKey: ["get-all"] }),
    onError: (err) =>
      notifications.show({ title: err.title, message: err.detail, color: "red", icon: <IconAlertSmall /> })
  });

  const form = useForm({
    validate: zodResolver(noteSchemas.updateNoteDtoSchema),
    initialValues: { name: note.name, text: note.text }
  });

  const updateButton = (values: UpdateNoteDto) => {
    updateNote.mutate({ id: note.id, body: { name: values.name, text: values.text } });
    close();
  };

  return (
    <>
      <Modal title="Update note" opened={opened} onClose={close} overlayProps={{ opacity: 0.55, blur: 3 }}>
        <form onSubmit={form.onSubmit(updateButton)}>
          <TextInput mb="sm" label="Name" placeholder="Name" {...form.getInputProps("name")} />
          <Textarea mb="sm" label="Text" placeholder="Text" {...form.getInputProps("text")} />

          <Group position="apart" mt="xl">
            <Button type="submit" color="blue">
              Update note
            </Button>
            <Button onClick={form.reset} color="gray">
              Reset
            </Button>
          </Group>
        </form>
      </Modal>
      <Button onClick={open} variant="light" color="blue">
        Update note
      </Button>
    </>
  );
};
