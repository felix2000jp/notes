import { Center, Grid } from "@mantine/core";
import { useQuery } from "@tanstack/react-query";
import { ErrorAlert } from "~/components/error-alert";
import { LoadingCircle } from "~/components/loading-circle";
import { AddNoteModal } from "~/components/note-components/add-note-modal";
import { NoteCard } from "~/components/note-components/note-card";
import { noteServices } from "~/services/note-services";

export const Home = () => {
  const getAll = useQuery({ queryKey: ["get-all"], queryFn: noteServices.getAll });

  if (getAll.isLoading) return <LoadingCircle />;
  if (getAll.isError) return <ErrorAlert error={getAll.error} />;

  return (
    <>
      <Center m="md">
        <AddNoteModal />
      </Center>
      <Grid m="lg">
        {getAll.data?.map((note, index) => (
          <Grid.Col key={index} md={6} lg={4}>
            <NoteCard note={note} />
          </Grid.Col>
        ))}
      </Grid>
    </>
  );
};
