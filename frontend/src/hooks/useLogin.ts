import { useMutation } from "@tanstack/react-query";
import { setCookie } from "cookies-next";
import { useRouter } from "next/router";
export interface LoginRequest {
    email: string;
    password: string;
}

async function postLogin(credentials: LoginRequest) {
    let now = new Date();
    let time = now.getTime();
    time += 3600 * 1000;
    now.setTime(time);
    const res = await fetch("https://localhost:59745/api/templateauthentication/login", { method: "POST", headers: { "Content-Type": "application/json" }, body: JSON.stringify(credentials) });

    if (res?.ok) {
        setCookie('accessToken', "bearer " + await res.text(), {expires: now});
        return;
    }
    console.log(await res.json())
    throw new Error("Something went wrong on the server")
}


function useLogin() {
    return useMutation({ mutationKey: ["login"], mutationFn: postLogin });
}


export default useLogin;