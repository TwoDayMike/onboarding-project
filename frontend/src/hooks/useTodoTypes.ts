import { useQuery } from "@tanstack/react-query";
import { TodoTypeExampleDTO } from "services/backend/client.generated";

async function fetchTodoTypes(): Promise<TodoTypeExampleDTO[]> {
    const res = await fetch("https://localhost:59745/api/TemplateTodoType/Get", { method: "GET" });
    return res.json();
}


function useTodoTypes() {
    return useQuery({ queryKey: ["todoTypes"], queryFn: fetchTodoTypes });
}

export default useTodoTypes;