import { Alert, Center } from "@mantine/core";
import { IconAlertCircleFilled } from "@tabler/icons-react";
import { ProblemDetails } from "~/utils/problem-details";

type ErrorComponent = { error: ProblemDetails };

export const ErrorAlert = ({ error }: ErrorComponent) => {
  return (
    <Center m="md">
      <Alert icon={<IconAlertCircleFilled size="1.5rem" />} title={error.title} color="red">
        {error.detail}
      </Alert>
    </Center>
  );
};
