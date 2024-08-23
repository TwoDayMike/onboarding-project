import { Suspense } from "react";
import Select from "./UI/Select";
import useTodoTypes from "hooks/useTodoTypes";
import { UseFormRegisterReturn } from "react-hook-form";

interface TodoTypeDropdown{
    registerTodo : UseFormRegisterReturn<"todoTypeId">;
}

function TodoTypeDropDown(props : TodoTypeDropdown) {
    const { data, isLoading, isError } = useTodoTypes();
    console.log(data)

    if (isLoading)
        return <Suspense />

    if (isError)
        return <div>Something went wrong on the server.</div>

    return (
        <Select title="todoType">
            {data.map((e) => {
                return (<option {...props.registerTodo} key={e.id} value={Number(e.id)}>
                    {e.name}
                </option>)
            })}
        </Select>
    )
}

export default TodoTypeDropDown