using System;

namespace core.models.RequestResults;

public record LoginRequest (
    string Username,
    string Password
);