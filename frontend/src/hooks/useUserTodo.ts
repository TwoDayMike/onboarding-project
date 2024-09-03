import { useMutation, useQuery } from "@tanstack/react-query";
import { TodoExampleDTO } from "services/backend/client.generated";
import { getCookie } from "cookies-next";

const getUserTodos = async (token: string):Promise<TodoExampleDTO[]> => {
    const tokens = getCookie("accessToken");
    const res = await fetch("https://localhost:59745/api/TemplateAuthentication", {
        headers: {
            "Authorization": token.toString(),
            "Content-Type" : "application/json"
        },
        method: "GET"
    })
    if (res?.ok) {
        return await res.json();
    }

    throw Error("Something went wrong on the server.")
}



function useUserTodo(token: string) {
    return useQuery({ queryKey: ["userTodo"], queryFn: () => getUserTodos(token) });
  }

export default useUserTodo;