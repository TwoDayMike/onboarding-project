import { useQuery } from "@tanstack/react-query";
import { TodoExampleDTO } from "services/backend/client.generated";

async function fetchTodos ():Promise<TodoExampleDTO[]> {
    const res = await fetch("https://localhost:59745/api/TemplateTodo/Get", {method: "GET"});
    return res.json();
}


function useTodos() {
    return useQuery({queryKey: ["todos"], queryFn: fetchTodos});
}

export default useTodos;