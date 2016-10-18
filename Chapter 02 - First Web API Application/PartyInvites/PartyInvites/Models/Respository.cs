using System.Collections.Generic;

namespace PartyInvites.Models {
    public class Repository {
        private static Dictionary<string, GuestResponse> responses;

        static Repository() {
            responses = new Dictionary<string, GuestResponse>();
            responses.Add("Bob", new GuestResponse {
                Name = "Bob",
                Email = "bob@example.com", WillAttend = true
            });
            responses.Add("Alice", new GuestResponse {
                Name = "Alice",
                Email = "alice@example.com", WillAttend = true
            });
            responses.Add("Paul", new GuestResponse {
                Name = "Paul",
                Email = "paul@example.com", WillAttend = true
            });
        }

        public static void Add(GuestResponse newResponse) {
            string key = newResponse.Name.ToLowerInvariant();
            if (responses.ContainsKey(key)) {
                responses[key] = newResponse;
            } else {
                responses.Add(key, newResponse);
            }
        }

        public static IEnumerable<GuestResponse> Responses {
            get { return responses.Values; }
        }
    }
}
