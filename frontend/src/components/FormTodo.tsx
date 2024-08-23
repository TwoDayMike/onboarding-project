import usePostTodo from "hooks/usePostTodo";
import { useState } from "react"
import { SubmitHandler, useForm } from "react-hook-form";
import TodoTypeDropDown from "./TodoTypeDropDown";
type formType = "Todo" | "TodoType";

interface todoInputs {
    name: string;
    description: string;
    todoTypeId: number;
}
function FormTodo() {
    const [formType, setFormType] = useState<formType>("Todo")
    const {register, handleSubmit} = useForm();
    const {register: registerTodo, handleSubmit: handleSubmitTodo} = useForm<todoInputs>();
    const {mutateAsync} = usePostTodo();

    const onTodoSubmit : SubmitHandler<todoInputs> = async (data, event) => {
        event.preventDefault();
        console.log(data)
        data["todoTypeId"] = Number(data["todoTypeId"])
        console.log(data)
        await mutateAsync(data);
    }
    return (
        <div style={{ display: "flex", justifyContent: "center", flexDirection: "column", alignItems: "center", marginBottom: 50 }}>
            <div style={{ display: "flex", gap: 15 }}>
                <button style={{ width: "100px", padding: "10px", background: "blue", border: "none", borderRadius: 18 }} onClick={() => setFormType("TodoType")}>Set TodoType</button>
                <button style={{ width: "100px", padding: "10px", background: "green", border: "none", borderRadius: 18 }} onClick={() => setFormType("Todo")}>Set Todo</button>
            </div>
            <div style={{ padding: 10 }}>{formType}</div>
            {formType === "Todo" ?
                <form onSubmit={handleSubmitTodo(onTodoSubmit)} style={{ display: 'flex', flexDirection: 'column' }}>
                    <label>Name</label>
                    <input {...registerTodo("name")} type="text"></input>
                    <label>Description</label>
                    <input {...registerTodo("description")} type="text"></input>
                    <label>Name</label>
                    <TodoTypeDropDown registerTodo={registerTodo("todoTypeId")}/>
                    <button style={{ margin: 20, background: "green", border: "none", padding: 8, borderRadius: 12 }}>Add</button>
                </form>
                : null}

            {formType === "TodoType" ?
                <form style={{ display: 'flex', flexDirection: 'column' }}>
                    <label>Name</label>
                    <input type="text"></input>
                    <button  style={{ margin: 20, background: "green", border: "none", padding: 8, borderRadius: 12 }}>Add</button>
                </form>
                : null}
        </div>
    )
}

export default FormTodo;