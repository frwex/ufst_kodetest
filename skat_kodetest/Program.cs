using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace skat_kodetest
{
    public class Program
    {
        static void Main(string[] args)
        {
            Task t = GetDataAndCalculate();
            t.Wait();
        }

        static async Task GetDataAndCalculate()
        {
            var url = "http://13.74.31.101/api/points";

            using (var client = new Client())
            {
                var json = await client.Get<GameDataDto>(url);
                try
                {
                    var scores = ScoreGame(json.points);

                    var scoredData = new ScoredGameDto() { token = json.token, points = scores };
                    var response = await client.Post<HttpResponseMessage>(url, scoredData);

                    if(response.StatusCode != System.Net.HttpStatusCode.OK)
                        Console.WriteLine("Something went wrong, perhaps the calculated result(s) were incorrect");
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Data);
                }
            }

            Console.ReadLine();
        }
        
        public static List<int> ScoreGame(List<List<int>> frames)
        {
            if (frames == null)
                return null;

            var results = new List<int>();

            try
            {
                int previousScore = 0;
                for (int i = 0; i < frames.Count(); i++)
                {
                    var frame = frames[i];
                    if (frame == null || frame.Count == 0)
                        return null;
                    
                    if (CalculateFrameSum(frame) < 10) // not a spare nor strike
                    {
                        results.Add(previousScore + CalculateFrameSum(frame));
                    }
                    
                    if (!IsFrameStrike(frame) && CalculateFrameSum(frame) == 10) // spare
                    {
                        results.Add(previousScore + 10 + GetNextPinSum(frames, i));
                    }
                    
                    if (IsFrameStrike(frame)) // strike;
                    {
                        results.Add(previousScore + 10 + GetNextFrameSum(frames, i));
                    }

                    previousScore = results.Last();
                }
            }
            catch(IndexOutOfRangeException e)
            {
                Console.WriteLine(e.Data);
            }

            return results;
        }

        static bool IsFrameStrike(List<int> frame)
        {
            if (frame == null)
                return false;

            return frame[0] == 10;
        }

        static bool IsFrameSpare(List<int> frame)
        {
            if (frame == null || IsFrameStrike(frame))
                return false;

            return frame.Take(2).Sum() == 10;
        }

        static int CalculateFrameSum(List<int> frame)
        {
            if (frame == null)
                return 0;

            return frame[0] + frame[1];
        }

        static int GetNextPinSum(List<List<int>> frames, int current)
        {
            if (frames == null)
                return 0;

            if (current + 1 >= frames.Count() || frames[current + 1] == null) // Failsafe check.
                return 0; // Don't add value here, the bonus added in if-clause will count as the points. Last throw does not award bonus points

            return frames[current + 1][0];
        }

        static int GetNextFrameSum(List<List<int>> frames, int current)
        {
            if (frames == null)
                return 0;

            if (current + 1 >= frames.Count() || frames[current + 1] == null) // If last throw is a strike, we just want to sum current frame
                return frames[current].Take(2).Sum();

            if(IsFrameStrike(frames[current + 1])) // If this frame and the next are strikes, we want the third frame's first pin count
            {
                if(current + 2 < frames.Count())
                    return frames[current + 1][0] + frames[current + 2][0]; // Get the next two pins
            }

            return frames[current + 1].Take(2).Sum(); // If the last throw is [10, 10] we want them both regardless, according to the assingment specification.
        }
    }
}