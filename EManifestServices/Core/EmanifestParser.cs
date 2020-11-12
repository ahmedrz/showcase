using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace EManifestServices.Core
{
    public class EmanifestParser
    {
        Stream fileStream;
        public EmanifestParser(Stream fileStream)
        {
            this.fileStream = fileStream;
        }

        public VoyageDetails Parse()
        {
            if (fileStream == null)
            {
                throw new Exception("File stream is null");
            }
            object lastObject = null;

            VoyageDetails voyage = new VoyageDetails();
            BOLDetails lastBol;
            ContainerDetails lastContainer;
            string line;
            using (StreamReader reader = new StreamReader(fileStream))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith("VOY"))
                    {
                        voyage = VoyageParser.Parse(line);
                        lastObject = voyage;
                    }
                    else if (line.StartsWith("BOL"))
                    {
                        var bol = BolParser.Parse(line);
                        lastObject = lastBol = bol;

                    }
                    else if (line.StartsWith("CTR"))
                    {
                        var container = ContainerParser.Parse(line);
                        lastObject = lastContainer = container;
                    }
                    else if (line.StartsWith("CON"))
                    {
                        var consignment = ConsignmentParser.Parse(line);
                    }
                }
            }
            return voyage;
        }
    }
}