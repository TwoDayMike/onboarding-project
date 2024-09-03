import { useMutation, useQueryClient } from "@tanstack/react-query";
import { useToken } from "./useToken";

async function PutAssignee(todoId: number) {
    const {token} = useToken();
    const res = await fetch("https://localhost:59745/api/TemplateTodo/AssigneTodo", {method: "PUT", headers: {'Content-Type': 'application/json', 'Authorization': token?.toString()}, body: JSON.stringify({todoId})});
    return;
}

function useAssign() {
    const queryClient = useQueryClient();
    return useMutation({mutationKey: ["deleteTodo"], mutationFn: PutAssignee, onSuccess: () => {
        queryClient.invalidateQueries({queryKey: ["todos"]})
    }});
}

export default useAssign