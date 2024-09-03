import { User } from "hooks/useGetUsers";
import { Button } from "./UI/Button";
import { Dialog, DialogContent, DialogDescription, DialogFooter, DialogHeader, DialogTitle, DialogTrigger } from "./UI/Dialog"
import { Input } from "./UI/Input";
import { SubmitHandler, useForm } from "react-hook-form";
import { DialogClose } from "@radix-ui/react-dialog";

interface EditoUserDialogProps {
    user: User
}

interface EditUserInput {
    email?: string;
    firstName?: string;
    lastName?: string;
}

const EditUserDialog: React.FC<EditoUserDialogProps> = ({user}) => {
    const { register, handleSubmit } = useForm<EditUserInput>();
    const onSubmit: SubmitHandler<EditUserInput> = (data, event) => {
        console.log(data)
    }
    
    return (
        <Dialog>
            <DialogTrigger className="bg-slate-600 p-3.5 text-white rounded-md">Update</DialogTrigger>
            <DialogContent>
                <DialogHeader>
                    <DialogTitle>Editing...</DialogTitle>
                    <DialogDescription>
                        Make changes to your profile here. Click save when you're done.
                    </DialogDescription>
                </DialogHeader>
                <form onSubmit={handleSubmit(onSubmit)}>
                    <div className="flex flex-col gap-4">
                        <div>
                            <label htmlFor="firstName">First Name</label>
                            <Input {...register("firstName")} id="firstName" />
                        </div>
                        <div>
                            <label className="text-center" htmlFor="lastName">Last Name</label>
                            <Input {...register("lastName")} id="lastName" />
                        </div>
                        <div>
                            <label htmlFor="email">Email</label>
                            <Input {...register("email")} id="email" />
                        </div>
                    </div>
                    <DialogFooter className="mt-5">
                        <DialogClose asChild>
                            <Button type="submit">Save Changes</Button>
                        </DialogClose>
                    </DialogFooter>
                </form>
            </DialogContent>
        </Dialog>


    )
}

export default EditUserDialog;