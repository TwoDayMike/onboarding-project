import usePostTodo from "hooks/usePostTodo";
import { useState } from "react"
import { SubmitHandler, useForm } from "react-hook-form";
import TodoTypeDropDown from "./TodoTypeDropDown";
import { Tabs, TabsContent, TabsList, TabsTrigger } from "./UI/Tabs";
type formType = "Todo" | "TodoType";

interface todoInputs {
    name: string;
    description: string;
    todoTypeId: number;
}
function FormTodo() {
    const [formType, setFormType] = useState<formType>("Todo")
    const { register, handleSubmit } = useForm();
    const { register: registerTodo, handleSubmit: handleSubmitTodo } = useForm<todoInputs>();
    const { mutateAsync } = usePostTodo();

    const onTodoSubmit: SubmitHandler<todoInputs> = async (data, event) => {
        event.preventDefault();
        console.log(data)
        data["todoTypeId"] = Number(data["todoTypeId"])
        console.log(data)
        await mutateAsync(data);
    }
    return (
        <Tabs className="flex flex-col bg-transparent justify-center my-4">
            <TabsList>
                <TabsTrigger value={"Todo"}>Todo type</TabsTrigger>
                <TabsTrigger value={"TodoType"}>Todo</TabsTrigger>
            </TabsList>
            <TabsContent value="Todo"> 
                <form className="flex flex-col gap-2" onSubmit={handleSubmitTodo(onTodoSubmit)} >
                    <label className="text-center">Name</label>
                    <input {...registerTodo("name")} type="text"></input>
                    <label className="text-center">Description</label>
                    <input {...registerTodo("description")} type="text"></input>
                    <label className="text-center">Type</label>
                    <TodoTypeDropDown registerTodo={registerTodo("todoTypeId")} />
                    <button >Add</button>
                </form>
            </TabsContent>
            <TabsContent value="TodoType">
                <form>
                    <label>Name</label>
                    <input type="text"></input>
                    <button>Add</button>
                </form>
            </TabsContent>
        </Tabs>
    )
}

export default FormTodo;