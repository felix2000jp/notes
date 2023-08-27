import { Button, Group, Modal, Text } from "@mantine/core";
import { useDisclosure } from "@mantine/hooks";
import { notifications } from "@mantine/notifications";
import { IconAlertSmall } from "@tabler/icons-react";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { noteServices } from "~/services/note-services";

type RemoveNoteModal = { noteId: string };

export const RemoveNoteModal = ({ noteId }: RemoveNoteModal) => {
  const queryClient = useQueryClient();
  const [opened, { open, close }] = useDisclosure(false);

  const removeNote = useMutation({
    mutationKey: ["remove"],
    mutationFn: noteServices.remove,
    onSuccess: () => queryClient.invalidateQueries({ queryKey: ["get-all"] }),
    onError: (error) =>
      notifications.show({
        title: error.title,
        message: error.detail,
        color: "red",
        icon: <IconAlertSmall />
      })
  });

  const deleteButton = () => {
    removeNote.mutate({ id: noteId });
    close();
  };

  return (
    <>
      <Modal title="Delete note" opened={opened} onClose={close} overlayProps={{ opacity: 0.55, blur: 3 }}>
        <Text>Are you sure you want to delete this note?</Text>
        <Group position="center" mt="xl">
          <Button onClick={deleteButton} color="red">
            Delete note
          </Button>
        </Group>
      </Modal>
      <Button onClick={open} variant="light" color="red">
        Delete note
      </Button>
    </>
  );
};
