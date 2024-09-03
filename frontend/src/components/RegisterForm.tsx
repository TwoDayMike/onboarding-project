import { SubmitHandler, useForm } from "react-hook-form"
import Form from "./UI/Form"
import { Button } from "./UI/Button";
import { Input } from "./UI/Input";
import useRegister, { RegisterRequest } from "hooks/useRegister";
import { toast } from "./UI/useToast";
import { useRouter } from "next/router";



const RegisterForm: React.FC = () => {
    const { register, handleSubmit } = useForm<RegisterRequest>();
    const { mutateAsync } = useRegister();
    const router = useRouter();
    const onSubmit: SubmitHandler<RegisterRequest> = async (data, event) => {
        console.log(data)
        await mutateAsync(data).then((data) => {
            console.log(data)
            router.push("/login")
            toast({
                title: "Succesfully logged in",
                description: data
            })
        }).catch((e) => {
            toast({
                variant: "destructive",
                title: "Unsuccessful login attempt",
                description: e.toString()

            })
        });
    }
    return (
        <Form classNames={"flex flex-col gap-4 w-1/3 h-auto justify-evenly p-3 border-slate-400 border-2"} handleSubmit={handleSubmit(onSubmit)}>
            <h3 className="text-center text-xl">Login</h3>
            <div className=" w-2/3 flex flex-col mx-auto ">
                <label className="" htmlFor="email">Email</label>
                <Input {...register("email")} placeholder="Email" id="email" type="text" />
            </div>
            <div className="flex text-center gap-4 justify-center mx-2">
                <div className="w-1/3">
                    <label>First Name</label>
                    <Input {...register("firstName")} placeholder="First Name" />
                </div>
                <div className="w-1/3">
                    <label>Last Name</label>
                    <Input {...register("lastName")} placeholder="Last Name" />
                </div>

            </div>
            <div className="flex text-center gap-4 justify-center mx-2">
                <div className="w-1/3">
                    <label>Password</label>
                    <Input {...register("password")} placeholder="Password" id="password" type="text" />
                </div>
                <div className="w-1/3">
                    <label>Confirm Password</label>
                    <Input {...register("confirmPassword")} placeholder="Confirm Password" id="password" type="text" />
                </div>

            </div>
            <Button className="border mx-auto p-2 rounded-lg hover:bg-blue-500">Login</Button>
            <a href="/login">Already have an account?</a>
        </Form>
    )
}

export default RegisterForm;