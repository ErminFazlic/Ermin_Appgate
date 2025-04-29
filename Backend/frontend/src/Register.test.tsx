import { render, screen } from "@testing-library/react";
import '@testing-library/jest-dom';
import Register from "./Register";

describe("Register Component", () => {
    beforeEach(() => {
        // Clear any mocks or setup before each test
        jest.clearAllMocks();
    });

    test("renders the registration form", () => {
        render(<Register />);
        expect(screen.getByLabelText(/username/i)).toBeInTheDocument();
        expect(screen.getByLabelText(/password/i)).toBeInTheDocument();
        expect(screen.getByRole("button", { name: /register/i })).toBeInTheDocument();
    });

});