import { Suspense } from "react";
import {Select, SelectTrigger} from "./UI/Select";
import useTodoTypes from "hooks/useTodoTypes";
import { UseFormRegisterReturn } from "react-hook-form";
import { SelectContent, SelectItem, SelectValue } from "@radix-ui/react-select";

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
        <Select>
            <SelectTrigger>
                <SelectValue placeholder="Choose a todo type or todo area"></SelectValue>
            </SelectTrigger>
            <SelectContent>
                {data.map((todoType) => {
                    return (<SelectItem key={todoType.id} value={(String(todoType.id))}>{todoType.name}</SelectItem>)
                })}
            </SelectContent>
        </Select>
    )
}

export default TodoTypeDropDown