import { Button, Group, Modal, TextInput, Textarea } from "@mantine/core";
import { useForm, zodResolver } from "@mantine/form";
import { useDisclosure } from "@mantine/hooks";
import { notifications } from "@mantine/notifications";
import { IconAlertSmall } from "@tabler/icons-react";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { AddNoteDto, noteSchemas } from "~/schemas/note-schemas";
import { noteServices } from "~/services/note-services";

export const AddNoteModal = () => {
  const [opened, { open, close }] = useDisclosure(false);
  const queryClient = useQueryClient();

  const addNote = useMutation({
    mutationKey: ["add"],
    mutationFn: noteServices.add,
    onSuccess: () => queryClient.invalidateQueries({ queryKey: ["get-all"] }),
    onError: (error) =>
      notifications.show({
        title: error.title,
        message: error.detail,
        color: "red",
        icon: <IconAlertSmall />
      })
  });

  const form = useForm({
    validate: zodResolver(noteSchemas.addNoteDtoSchema),
    initialValues: { name: "", text: "" }
  });

  const addButton = (values: AddNoteDto) => {
    addNote.mutate({ body: { name: values.name, text: values.text } });
    close();
  };

  return (
    <>
      <Modal title="Add note" opened={opened} onClose={close} overlayProps={{ opacity: 0.55, blur: 3 }}>
        <form onSubmit={form.onSubmit(addButton)}>
          <TextInput mb="sm" label="Name" placeholder="Name" {...form.getInputProps("name")} />
          <Textarea mb="sm" label="Text" placeholder="Text" {...form.getInputProps("text")} />

          <Group position="apart" mt="xl">
            <Button type="submit" color="green">
              Add note
            </Button>
            <Button onClick={form.reset} color="gray">
              Reset
            </Button>
          </Group>
        </form>
      </Modal>
      <Button onClick={open} color="green">
        Add note
      </Button>
    </>
  );
};
