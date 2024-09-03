import { useMutation } from "@tanstack/react-query";

export interface RegisterRequest {
    email: string;
    password: string;
    confirmPassword: string;
    firstName: string;
    lastName: string;
}
const postRegister = async (request: RegisterRequest) => {
    const successMessage = "Registration was succesfull but no data was returned from api."
    const res = await fetch("https://localhost:59745/api/TemplateAuthentication/Register", {
        method: "POST", headers: {
            "Content-Type": "application/json"
        }, body: JSON.stringify(request)
    });
    if (res.ok) {
        return successMessage
    }

    throw Error("Something went wrong on server.")
}


function useRegister() {
    return useMutation({ mutationKey: ["register"], mutationFn: postRegister })
}

export default useRegister;