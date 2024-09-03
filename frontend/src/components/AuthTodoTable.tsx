import usePutTodo, { PutTodoRequest } from "hooks/usePutTodo";
import { useToken } from "hooks/useToken"
import useUserTodo from "hooks/useUserTodo";
import { useEffect } from "react";
import { toast } from "./UI/useToast";
import { Checkbox } from "./UI/Checkbox";
import { Table, TableBody, TableCaption, TableCell, TableHead, TableHeader, TableRow } from "./UI/Table";
import LoadingSpinner from "./UI/LoadingSpinner";

interface AuthTodoTableProps {
    token: string;
}

function AuthTodoTable({ token }: AuthTodoTableProps) {

    const { data, isLoading, isSuccess } = useUserTodo(token)
    const { mutateAsync } = usePutTodo();
    console.log(token)

    function handleUpdateIsComplete(request: PutTodoRequest) {
        mutateAsync(request).then(() => {
            if (request.isCompleted) {
                return toast({
                    title: "Updated Todo was a success",
                    description: "Todo with id " + request.id + " has been marked as completed."
                })
            }
            return toast({
                title: "Updated Todo was a success",
                description: "Todo with id " + request.id + " has been marked as incomplete."
            })

        })
    }

    if (isLoading) {
        return <LoadingSpinner position="center"/>
    }

    if (!isSuccess) {
        return <div>Not able to get todos</div>
    }

    if (data.length === 0) {
        return <>No todos assigned to u</>
    }
    return (
        <Table>
            <TableCaption >A list of my todos</TableCaption>
            <TableHeader>
                <TableRow>
                    <TableHead>Id</TableHead>
                    <TableHead>Description</TableHead>
                    <TableHead>Name</TableHead>
                    <TableHead>Type</TableHead>
                    <TableHead>Completed</TableHead>
                </TableRow>
            </TableHeader>
            <TableBody>

                {data.map((todo) => {
                    return (
                        <TableRow key={todo.id}>
                            <TableCell>{todo.id}</TableCell>
                            <TableCell>{todo.description}</TableCell>
                            <TableCell>{todo.name}</TableCell>
                            <TableCell>{todo.typeName}</TableCell>
                            <TableCell><Checkbox onCheckedChange={(e) => {handleUpdateIsComplete({id: todo.id, isCompleted: e as boolean})}} checked={todo.isCompleted}></Checkbox></TableCell>
                        </TableRow>
                    )
                })}

            </TableBody>
        </Table>
    )
}

export default AuthTodoTable;