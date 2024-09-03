import useUsers, { User } from "hooks/useGetUsers";
import { Table, TableBody, TableCaption, TableCell, TableHead, TableHeader, TableRow } from "./UI/Table"
import LoadingSpinner from "./UI/LoadingSpinner";
import { Button } from "./UI/Button";
import { useToken } from "hooks/useToken";
import useDeleteUser from "hooks/useDeleteUser";
import EditUserDialog from "./EditUserDialog";
import { useState } from "react";

const UserTable: React.FC = () => {
    const { data, isLoading, } = useUsers();
    const { userObject } = useToken();
    const { mutateAsync } = useDeleteUser();
    const [selectedUser, setSelectedUser] = useState<User | null>();

    const handleUserSelection = (user: User) => {
        setSelectedUser(user);

    }
    if (isLoading)
        return <LoadingSpinner position="center" />

    return (
        <>
            <Table>
                <TableCaption>List of application users</TableCaption>
                <TableHeader>
                    <TableRow>
                        <TableHead>Id</TableHead>
                        <TableHead>Role</TableHead>
                        <TableHead>First Name</TableHead>
                        <TableHead>Last Name</TableHead>
                        <TableHead>Delete</TableHead>
                        <TableHead>Update</TableHead>
                    </TableRow>
                </TableHeader>
                <TableBody>
                    {data.map((e) => {
                        return (
                            <TableRow key={e.email}>
                                <TableCell>{e.id}</TableCell>
                                <TableCell>{e.roleName}</TableCell>
                                <TableCell>{e.firstName}</TableCell>
                                <TableCell>{e.lastName}</TableCell>
                                <TableCell><Button onClick={() => mutateAsync(e.id)} disabled={Number(userObject?.sub) === e.id} variant="destructive">Delete</Button></TableCell>
                                <TableCell><Button asChild><EditUserDialog user={selectedUser} /></Button></TableCell>
                            </TableRow>
                        )
                    })}
                </TableBody>

            </Table>

        </>
    )

}
export default UserTable;

