using AdventOfCode;
using System.Numerics;

namespace AdventOfCode2021
{
    public class Day19 : DayBase
    {
        private PriorityQueue<MatchedDistances, int> matchedDistances;
        private List<Scanner> scanners;
        private List<Guid> beaconIds;
        private List<Beacon> beacons;


        public void Parse(string[] data)
        {
            scanners = new List<Scanner>();
            beacons = new List<Beacon>();
            Scanner? scanner = null;
            foreach (string line in data)
            {
                if (line.StartsWith("---"))
                {
                    scanner = new Scanner();
                    scanner.Id = line.Replace("---", "").Replace("scanner", "").Trim().ToInt();
                    scanner.ObservedBeacons = new List<Beacon>();
                }
                else if (string.IsNullOrWhiteSpace(line) && scanner != null)
                {
                    foreach (Beacon beacon in scanner.ObservedBeacons)
                        beacon.CalcDistances(scanner.ObservedBeacons.Select(s => s.Position).ToList());

                    scanners.Add(scanner);
                    scanner = null;
                }
                else
                {
                    Beacon beacon = new Beacon();
                    int[] coords = line.Split(',').ToInt();
                    beacon.Position = new Vector3(coords[0], coords[1], coords[2]);
                    scanner?.ObservedBeacons.Add(beacon);
                    beacons.Add(beacon);
                }
            }
            if (scanner != null)
            {
                foreach (Beacon beacon in scanner.ObservedBeacons)
                    beacon.CalcDistances(scanner.ObservedBeacons.Select(s => s.Position).ToList());
                scanners.Add(scanner);
                scanner = null;
            }
        }

        public int NumberOfBeacons()
        {
            return beaconIds.Count;
        }

        public void ProcessQueue()
        {
            beaconIds = new List<Guid>();

            while (matchedDistances.Count > 0)
            {
                MatchedDistances pair = matchedDistances.Dequeue();
                if (pair.Beacon1.Id != null && pair.Beacon2.Id != null)
                {
                    if (pair.Beacon1.Id != pair.Beacon2.Id)
                    {
                        List<Beacon> beaconsToEdit = beacons.Where(b => b.Id == pair.Beacon2.Id).ToList();
                        foreach (Beacon beacon in beaconsToEdit)
                        {
                            beacon.Id = pair.Beacon1.Id;
                            beaconIds.Remove(pair.Beacon2.Id.Value);
                        }
                    }

                    continue;
                }

                if (pair.MatchCount == 0)
                {
                    pair.Beacon1.Id = pair.Beacon1.Id ?? Guid.NewGuid();
                    pair.Beacon2.Id = pair.Beacon2.Id ?? Guid.NewGuid();
                    if (!beaconIds.Contains(pair.Beacon1.Id.Value))
                        beaconIds.Add(pair.Beacon1.Id.Value);
                    if (!beaconIds.Contains(pair.Beacon2.Id.Value))
                        beaconIds.Add(pair.Beacon2.Id.Value);
                    continue;
                }

                Guid guid = pair.Beacon1.Id ?? pair.Beacon2.Id ?? Guid.NewGuid();
                pair.Beacon1.Id = guid;
                pair.Beacon2.Id = guid;

                if (!beaconIds.Contains(guid))
                    beaconIds.Add(guid);
            }
        }

        public void MatchAllBeacons()
        {
            matchedDistances = new PriorityQueue<MatchedDistances, int>();
            for (int i = 0; i < scanners.Count - 1; i++)
            {
                for (int j = i + 1; j < scanners.Count; j++)
                {
                    MatchBeacons(scanners[i], scanners[j]);
                }
            }
        }

        public void MatchBeacons(Scanner scanner1, Scanner scanner2)
        {
            foreach (Beacon observedBeacon1 in scanner1.ObservedBeacons)
            {
                foreach (Beacon observedBeacon2 in scanner2.ObservedBeacons)
                {
                    int sameDistances = observedBeacon1.DistanceToOthers.Intersect(observedBeacon2.DistanceToOthers).Count();
                    matchedDistances.Enqueue(new MatchedDistances
                    {
                        Beacon1 = observedBeacon1,
                        Beacon2 = observedBeacon2,
                        MatchCount = sameDistances,
                    }, 100 - sameDistances);
                }
            }
        }
    }

    public class MatchedDistances
    {
        public Beacon Beacon1 { get; set; }
        public Beacon Beacon2 { get; set; }
        public int MatchCount { get; set; }
    }

    public class Scanner
    {
        public int Id { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Orientation { get; set; }

        public int NumberOfIdentifiedBeacons
        {
            get
            {
                return ObservedBeacons.Where(b => b.Id != null).Count();
            }
        }

        public int CountCommon(Scanner other)
        {
            int count = 0;
            foreach (Beacon beacon in ObservedBeacons)
            {
                foreach (Beacon observedBeacon2 in other.ObservedBeacons)
                {
                    int sameDistances = beacon.DistanceToOthers.Intersect(observedBeacon2.DistanceToOthers).Count();
                    if (sameDistances >= 12)
                        count++;
                }
            }
            return count;
        }

        public bool MatchCommon(Scanner other)
        {
            int count = 0;
            foreach (Beacon beacon in ObservedBeacons)
            {
                foreach (Beacon observedBeacon2 in other.ObservedBeacons)
                {
                    int sameDistances = beacon.DistanceToOthers.Intersect(observedBeacon2.DistanceToOthers).Count();
                    if (sameDistances >= 10)
                        return false;

                }
            }
            return true;
        }


        public List<Beacon> ObservedBeacons { get; set; }

    }

    public class Beacon
    {
        public Guid? Id { get; set; }
        public Vector3 Position { get; set; }
        public List<int> DistanceToOthers { get; set; }

        public void CalcDistances(List<Vector3> beacons)
        {
            DistanceToOthers = new List<int>();
            foreach (Vector3 beacon in beacons)
            {
                if (Position == beacon)
                    continue;

                DistanceToOthers.Add((int)Vector3.DistanceSquared(Position, beacon));
            }
        }
    }
}
