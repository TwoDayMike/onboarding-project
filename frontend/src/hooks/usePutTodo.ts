import { useMutation, useQueryClient } from "@tanstack/react-query";

interface PutTodoRequest {
    id: number;
    isCompleted: boolean;
}

async function putTodo(request : PutTodoRequest) {
    console.log(request);
    const res = await fetch("https://localhost:59745/api/TemplateTodo/Update", {method: "PUT", headers: {
        'Content-Type' : "application/json"
    }, body: JSON.stringify(request)})
    return;
}


function usePutTodo() {
    const queryClient = useQueryClient();
    return useMutation({mutationKey: ["postTodo"], mutationFn: putTodo, onSuccess: () => {
        queryClient.invalidateQueries({queryKey: ["todos"]})
    }});
}

export default usePutTodo;