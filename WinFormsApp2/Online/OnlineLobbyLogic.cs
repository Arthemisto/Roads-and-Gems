namespace Indigo
{
    public static class OnlineLobbyLogic
    {
        public static List<string> MapPlayerColors(IEnumerable<string?> playerNames)
        {
            List<string> colors = new();

            foreach (string? rawName in playerNames)
            {
                string name = rawName?.Trim() ?? string.Empty;
                if (string.IsNullOrEmpty(name))
                {
                    colors.Add("White");
                    continue;
                }

                char lastLetter = char.ToUpperInvariant(name[^1]);
                string color = lastLetter switch
                {
                    'W' => "White",
                    'R' => "Red",
                    'C' => "Cyan",
                    'P' => "Purple",
                    _ => "White"
                };

                colors.Add(color);
            }

            return colors;
        }

        public static bool TryParseEndpoint(string rawValue, out string ip, out int port)
        {
            ip = rawValue;
            port = 0;

            if (rawValue.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
                rawValue = rawValue.Substring("https://".Length);
            else if (rawValue.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
                rawValue = rawValue.Substring("http://".Length);

            int separatorIndex = rawValue.LastIndexOf(':');
            if (separatorIndex <= 0 || separatorIndex >= rawValue.Length - 1)
                return false;

            string candidateIp = rawValue[..separatorIndex].Trim();
            string candidatePort = rawValue[(separatorIndex + 1)..].Trim();

            if (!int.TryParse(candidatePort, out int parsedPort))
                return false;

            if (parsedPort < 1024 || parsedPort > 65535)
                return false;

            ip = candidateIp;
            port = parsedPort;
            return true;
        }
    }
}
