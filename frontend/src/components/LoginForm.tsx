import { SubmitHandler, useForm } from "react-hook-form";
import Form from "./UI/Form";
import useLogin, { LoginRequest } from "hooks/useLogin";
import { useState } from "react";
import { useRouter } from "next/router";
import { useToast } from "./UI/useToast";
import { Input } from "./UI/Input";
import { Button } from "./UI/Button";

function LoginForm() {
    const { register, handleSubmit: handleLogin } = useForm<LoginRequest>();
    const {mutateAsync} = useLogin();
    const [error, setError] = useState<string | null>(null);
    const router = useRouter();
    const { toast } = useToast()
    
    const onSubmit: SubmitHandler<LoginRequest> = async (data, event) => {

        setError(null)
        const newDate = new Date();
        newDate.setHours(1)
        const retVal = newDate.toLocaleString("da-DK")
        event.preventDefault();
        await mutateAsync(data).then(() => {
            router.push("/me")
            toast({
                title: "Succesfully logged in",
                description: "Your session is limit to one hour from login time. Your estimated login period will end at " + retVal
            })
        }).catch(e => {
            toast({
                variant: "destructive",
                title: "Unsuccessful login attempt",
                description: e.toString()

            })
        });

    }
    return (
        <Form handleSubmit={handleLogin(onSubmit)} classNames="flex flex-col w-1/3 h-1/3 justify-evenly p-3 border-slate-400 border-2">
            <h3 className="text-center text-xl">Login</h3>
            {error && <div className="text-center text-red-500">{error}</div>}
            <label className="text-center" htmlFor="email">Email</label>
            <Input placeholder="Email" {...register("email", {required: true})} id="email" type="text" />
            <label className="text-center" htmlFor="password">Password</label>
            <Input placeholder="Password" {...register("password", {required: true})} id="password" type="text" />
            <Button className="border mx-auto p-2 rounded-lg hover:bg-blue-500">Login</Button>
            <a href="/register">Don't have an account?</a>
        </Form>
    )
}

export default LoginForm;