import { useMutation, useQueryClient } from "@tanstack/react-query";
import { useToken } from "./useToken";

const deleteUser = async (id : number) => {
    const { token } = useToken();
    const r = await fetch(`https://localhost:59745/api/TemplateUser/Delete?Id=${id}`, { method: "DELETE", headers: { "Content-Type": "application/json", "Authorization": token?.toString() } });
    console.log(r.status)
    if (r.ok && r.status === 204) {
        return;
    }

    if (r.status === 403) {
        return r.statusText;
    }

    throw new Error("Something went wrong on the server");
}


function useDeleteUser() {
    const queryClient = useQueryClient();
    return useMutation({
        mutationFn: deleteUser,
        mutationKey : ["deleteUser"],
        onSuccess: () => {
            queryClient.invalidateQueries({queryKey: ["users"]})
        }
    })
}

export default useDeleteUser;