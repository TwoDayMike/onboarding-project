import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { TodoExampleDTO } from "services/backend/client.generated";


interface TodoDTO {
    name: string;
    description: string;
    todoTypeId: number;
}
  
async function postTodo (request : TodoDTO):Promise<number> {
    const res = await fetch("https://localhost:59745/api/TemplateTodo/Create", {method: "POST", body: JSON.stringify(request), headers: {"Content-Type" : "application/json"} });
    return res.json();
}


function usePostTodo() {
    const queryClient = useQueryClient();
    return useMutation({mutationKey: ["postTodo"], mutationFn: postTodo, onSuccess: () => {
        queryClient.invalidateQueries({queryKey: ["todos"]})
    }});
}

export default usePostTodo;