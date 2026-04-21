public record LoginResponseDto(
    bool IsSuccess,
    string Token,
    DateTime Expiry,
    string Role
);