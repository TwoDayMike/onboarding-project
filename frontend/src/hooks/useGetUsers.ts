import { useQuery } from "@tanstack/react-query";
import { useToken } from "./useToken"
export interface User {
    id: number;
    firstName : string;
    lastName : string;
    roleName : string;
    email : string;
}

const getUsers = async (): Promise<User[]>  => {
    const { token } = useToken();
    const r = await fetch("https://localhost:59745/api/TemplateUser/Get", { method: "GET", headers: { "Authorization": token?.toString()} });
    if (r.ok){
        return r.json();
    }

    throw new Error("Something went wrong on the server.")   
}


function useUsers() {
    return useQuery({queryKey: ["users"], queryFn: getUsers});
}

export default useUsers;