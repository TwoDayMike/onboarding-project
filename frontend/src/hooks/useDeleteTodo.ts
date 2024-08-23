import { useMutation, useQueryClient } from "@tanstack/react-query";

async function deletePost(todoId: number) {
    const res = await fetch("https://localhost:59745/api/TemplateTodo/DeleteTodoById?TodoId=" + todoId, {method: "DELETE"});
    return;
}

function useDeleteTodo() {
    const queryClient = useQueryClient();
    return useMutation({mutationKey: ["deleteTodo"], mutationFn: deletePost, onSuccess: () => {
        queryClient.invalidateQueries({queryKey: ["todos"]})
    }});
}

export default useDeleteTodo