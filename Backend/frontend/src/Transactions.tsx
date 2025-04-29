import { Receipt } from "@mui/icons-material";
import { Container, CssBaseline, Box, Avatar, TextField, Typography, Button, Grid, List, ListItem, ListItemText } from "@mui/material";
import { useEffect, useState } from "react";
import { Link, useNavigate } from "react-router-dom";

const Transactions = () => {
    const [amount, setAmount] = useState<number>();
    const [balance, setBalance] = useState<number | null>(null);
    const [transactions, setTransactions] = useState<ITransaction[] | null>(null);
    const navigate = useNavigate();

    useEffect(() => {
        checkToken();
        getBalance();
        getTransactions();
    }, []);

    const handleSubmit = (isWithdrawal: boolean) => {
        fetch("https://localhost:7058/api/transactions", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Authorization": "Bearer " + localStorage.getItem("token")
            },
            body: JSON.stringify({
                amount,
                isWithdrawal
            }),
        }).then((response) => {
            if (response.ok) {
                alert("Created transaction");
                getBalance();
                getTransactions();
            } else {
                alert("Could not create transaction");
            }
        });
    };

    const getBalance = async () => {

        const response = await fetch("https://localhost:7058/api/balance", {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
                "Authorization": "Bearer " + localStorage.getItem("token")
            }
        });

        const data: number = parseInt(await response.text());

        if (response.ok) {
            setBalance(data);
        } else {
            setBalance(null);
        }
    };

    const getTransactions = async () => {

        const response = await fetch("https://localhost:7058/api/transactions", {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
                "Authorization": "Bearer " + localStorage.getItem("token")
            }
        });

        const data: ITransaction[] = await response.json();

        if (response.ok) {
            setTransactions(data);
        } else {
            setTransactions(null);
        }
    };

    const logout = () => {
        localStorage.removeItem("token");
    }

    const checkToken = () => {
        if (localStorage.getItem("token") == null) {
            navigate("/login");
        }
    }

    return (
        <>
            <Container maxWidth="xs">
                <CssBaseline />
                <Box
                    sx={{
                        mt: 20,
                        display: "flex",
                        flexDirection: "column",
                        alignItems: "center",
                    }}
                >
                    <Avatar sx={{ m: 1, bgcolor: "primary.light" }}>
                        < Receipt />
                    </Avatar>
                    <Typography variant="h5">{(balance != null ? "Balance: " + balance : "Could not fetch balance")}</Typography>
                    <Typography sx={{ mt: 3 }} variant="h5">{(transactions != null ? "Transactions" : "Could not fetch transactions")}</Typography>
                    <List sx={{ maxHeight: "500px", overflow: "auto" }}>
                        {transactions?.map(item =>
                            <ListItem>
                                <ListItemText primary={(item.isWithdrawal ? "Withdraw: -" : "Deposit: +") + item.amount + ", " + item.dateCreated.split("T")[0]} />
                            </ListItem>
                        )}
                    </List>
                    <TextField
                        margin="normal"
                        required
                        id="amount"
                        label="Amount"
                        type="number"
                        name="amount"
                        autoFocus
                        value={amount}
                        onChange={(e) => setAmount(parseInt(e.target.value))}
                    />
                    <Box sx={{ mt: 1 }}>
                        <Button
                            variant="contained"
                            sx={{ mt: 3, mb: 2, mr: 2 }}
                            onClick={() => { handleSubmit(false) }}
                        >
                            Deposit
                        </Button>
                        <Button
                            variant="contained"
                            sx={{ mt: 3, mb: 2 }}
                            onClick={() => { handleSubmit(true) }}
                        >
                            Withdraw
                        </Button>
                        <Grid container justifyContent={"flex-end"}>
                            <Grid>
                                <Link onClick={() => { logout() }} to="/login">Log out</Link>
                            </Grid>
                        </Grid>
                    </Box>
                </Box>
            </Container >
        </>
    );
};

export default Transactions;

interface ITransaction {
    dateCreated: string,
    amount: number,
    isWithdrawal: boolean
}
