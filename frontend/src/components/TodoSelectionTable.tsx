import useTodos from "hooks/useTodos"
import {Button} from "./UI/Button";
import Modal from "./UI/Modal";
import { TodoExampleDTO } from "services/backend/client.generated";
import { useState } from "react";
import LoadingSpinner from "./UI/LoadingSpinner";

import { Table, TableBody, TableCaption, TableCell, TableHead, TableHeader, TableRow } from "./UI/Table";
import useDeleteTodo from "hooks/useDeleteTodo";
import { Checkbox } from "./UI/Checkbox";
import usePutTodo, { PutTodoRequest } from "hooks/usePutTodo";
import { toast } from "./UI/useToast";

function TodoSelectionTable() {
    const { data, isLoading } = useTodos();
    const {mutateAsync} = useDeleteTodo();
    const [selectedTodo, setSelectedTodo] = useState<TodoExampleDTO | null>(null);
    const { mutateAsync: updateIsComplete } = usePutTodo();
    function onClose() {
        setSelectedTodo(null);

    }

    function handleUpdateIsComplete(request: PutTodoRequest) {
        updateIsComplete(request).then(() => {
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
        return (<LoadingSpinner position="center"/>)
    }
    return (
        <Table>
            <TableCaption>
                A list of all todos
            </TableCaption>
            <TableHeader>
                <TableRow>
                    <TableHead>Id</TableHead>
                    <TableHead>Description</TableHead>
                    <TableHead>Name</TableHead>
                    <TableHead>Type</TableHead>
                    <TableHead>Completed?</TableHead>
                    <TableHead>View</TableHead>
                    <TableHead>Delete</TableHead>
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
                            <TableCell><Checkbox checked={todo.isCompleted} onCheckedChange={(e) => {handleUpdateIsComplete({id: todo.id, isCompleted: e as boolean})}}/></TableCell>
                            <TableCell><Button variant="default" onClick={() => setSelectedTodo(todo)}>View</Button></TableCell>
                            <TableCell><Button variant="destructive" onClick={() => mutateAsync(todo.id)}>Delete</Button></TableCell>
                        </TableRow>
                    )
                })}
            </TableBody>
        </Table>
    )
}

export default TodoSelectionTable;