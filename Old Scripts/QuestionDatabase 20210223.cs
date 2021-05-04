using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quiz;		//our custom namespace

public static class QuestionDatabase {
    public static List<Q> generateQuestions() {
        List<Q> allQuestions = new List<Q>();
        //EXAMPLES:
        //simple(string title, string[] correctAnswers, string answerOverride, Difficulty difficulty)
        //Note: correctAnswer must match a brainpart. 
        //Note: answerOverride is optional. Default is correctAnswer[0].
		//Difficulty are optional. Default difficulty is normal.

        //box(string title, string[] correctAnswers, Difficulty difficulty)
        //Note: Difficulty is optional. 

        //region(string title, string correctAnswer, string answerOverride, Vector3 hitPoint, float distanceThreshold, Difficulty difficulty)
        //Note: answerOverride and difficulty are optional.
		//hitPoint = the position of the correct point in global space, when the desired part is in its original position. 
		//distanceThreshold = the radius of the sphere of correct points. Bigger = easier.

		//syntax example:
        allQuestions.Add(Questions.simple(
            "Which structure clearly shows the middle cerebellar peduncles?",	//title
            new string[]{"Brain Stem.CL", "Brain Stem.CR"},		//correctAnswers
			"The Brain Stem."));								//answerOverride

        allQuestions.Add(Questions.simple(
            "Which structure would contain the inferior olivary nuclei?",
            new string[] {"Brain Stem.CL", "Brain Stem.CR" },
            Difficulty.Easy));

        allQuestions.Add(Questions.simple(
            "Which structure contains the midbrain or mesencephalon?",
            new string[] {"Brain Stem.CL", "Brain Stem.CR" },
            Difficulty.Easy));

        allQuestions.Add(Questions.simple(
            "Which structure functions to control systems that regulate autonomic function, feeding, drinking, and thermoregulation?",
            "Hypothalamus",
            Difficulty.Easy));

        allQuestions.Add(Questions.simple(
            "The tuber cinereum and parts of the infundibulum are located just caudal to which structure?",
            "Optic Chiasm",
            Difficulty.Clinical));

        allQuestions.Add(Questions.simple(
            "Visual sensory fibers travel through this structure from the retina",
            "Optic Nerve",
            Difficulty.Easy));

        allQuestions.Add(Questions.simple(
            "At this structure, visual sensory fibers cross to the opposite side.",
            "Optic Chiasm",
            Difficulty.Easy));

        allQuestions.Add(Questions.simple(
            "Crossed and uncrossed fibers leaving from the retina use this structure to reach the thalamus.",
            "Optic Tract"));

        allQuestions.Add(Questions.simple(
            "Sensory information from the nasal epithelium terminate in this structure.",
            "Olfactory Tract",
            Difficulty.Easy));

        allQuestions.Add(Questions.simple(
            "Cells from this structure terminate in olfactory specific cortical areas including the uncus.",
            "Olfactory Tract"));

        allQuestions.Add(Questions.simple(
            "This structure is anterior to the central sulcus.",
            new string[] {"Frontal Lobe.CL", "Frontal Lobe.CR"},
            Difficulty.Easy));

        allQuestions.Add(Questions.simple(
            "The structure is posterior to the central sulcus.",
            new string[] {"Parietal Lobe.CR", "Parietal Lobe.CL" },
            Difficulty.Easy));

        allQuestions.Add(Questions.simple(
            "The structure is anterior to the fissure of Rolando.",
            new string[] {"Frontal Lobe.CL", "Frontal Lobe.CR"}));

        allQuestions.Add(Questions.simple(
            "This structure is posterior to the fissure of Rolando.",
            new string[] {"Parietal Lobe.CR", "Parietal Lobe.CL" }));

        allQuestions.Add(Questions.simple(
            "This structure is inferior to the lateral fissure.",
            new string[] {"Temporal Lobe.CL", "Temporal Lobe.CR"},
            Difficulty.Easy));

        allQuestions.Add(Questions.simple(
            "This structure is inferior to the Sylvian fissure.",
            new string[] {"Temporal Lobe.CL", "Temporal Lobe.CR" }));

        allQuestions.Add(Questions.simple(
            "This structure is superior to the Sylvian fissure.",
             new string[] {"Frontal Lobe.CL", "Frontal Lobe.CR" }));

        allQuestions.Add(Questions.simple(
            "This structure is superior to the lateral sulcus.",
             new string[] {"Frontal Lobe.CL", "Frontal Lobe.CR" },
            Difficulty.Easy));

        allQuestions.Add(Questions.simple(
            "This structure is buried and is the fifth lobe of the hemisphere.",
            "Insula"));

        allQuestions.Add(Questions.simple(
            "This structure contains Broca's area, which produces written, spoken, or signed language.",
            "Frontal Lobe.CL",
            Difficulty.Easy));

        allQuestions.Add(Questions.simple(
            "This structure contains the Wernicke's area, permits comprehension of written, spoken, or signed language.",
            "Temporal Lobe.CL",
            Difficulty.Easy));

        allQuestions.Add(Questions.simple(
            "Control of movement from the left side of the body would be found in which cortical structure?",
            "Frontal Lobe.CR",
            Difficulty.Easy));

        allQuestions.Add(Questions.simple(
            "Sensory information from the left side of the body terminates in which cortical structure?",
            "Parietal Lobe.CR",
            Difficulty.Easy));

        allQuestions.Add(Questions.simple(
            "Damage to which cortical structure can produce striking changes to personality, eye movements, and impulse control.",
             new string[] {"Frontal Lobe.CL", "Frontal Lobe.CR" },
            Difficulty.Easy));

        //what is parietal complex in hierarchy?? //Parietal Lobe
        allQuestions.Add(Questions.simple(
            "Caudal to the post-central gyrus is this structure.",
            new string[] {"Parietal Lobe.CR", "Parietal Lobe.CL" },
            Difficulty.Easy));

        allQuestions.Add(Questions.simple(
            "The calcarine fissure is found in this structure.",
            new string[] {"Occipital Lobe.CL", "Occipital Lobe.CR"}));

        allQuestions.Add(Questions.simple(
            "This cortical structure receives gustatory and visceral sensory inputs?",
            "Insula",
            Difficulty.Clinical));

        allQuestions.Add(Questions.simple(
            "This fiber bundle interconnects limbic structures mainly from the temporal lobes.",
            "Anterior Commissure"));

        //What is the Hippocampus in the hierarchy? //Hippocampal Formation
        allQuestions.Add(Questions.simple(
            "This limbic structure runs along the floor of the lateral ventricle.",
            "Hippocampal Formation"));

        allQuestions.Add(Questions.simple(
            "This structure separates the lateral ventricles",
            "Septum Pellucidum",
            Difficulty.Easy));

        //Which Choroid Plexus?** //any of the choroid plexus would be correct
        allQuestions.Add(Questions.simple(
            "This structure is responsible for the synthesis of cerebrospinal fluid.",
            new string[] {"Choroid plexus of left lateral ventricle", "Choroid plexus of right lateral ventricle", "Choroid plexus of fourth ventricle"},
            "Choroid plexus",
            Difficulty.Easy));

        allQuestions.Add(Questions.simple(
            "The columns of the fornix split at this structure.",
            "Anterior Commissure"));

        allQuestions.Add(Questions.simple(
            "This structure sits between the claustrum and lateral ventricle.",
            "Caudate"));

        allQuestions.Add(Questions.simple(
            "This structure has both a head and tail and runs along the length of the lateral ventricle",
            "Caudate"));

        allQuestions.Add(Questions.simple(
            "The zone of confluence between the caudate and putamen is known as the...",
            "Nucleus Accumbens"));

        allQuestions.Add(Questions.simple(
            "This structure receives input from the cortex and projects to the globus pallidus and substantia nigra.",
            "Nucleus Accumbens",
            Difficulty.Clinical));

        allQuestions.Add(Questions.simple(
            "Internal capsule fibers separate this structure from the globus pallidus and putamen.",
            "Caudate",
            Difficulty.Clinical));

        allQuestions.Add(Questions.simple(
            "What structure is divided by the medial medullary lamina?",
            "Globus Pallidus",
            Difficulty.Clinical));

        allQuestions.Add(Questions.simple(
            "The lateral medullary lamina separates the globus pallidus from this structure.",
            "Putamen",
            Difficulty.Clinical));

        allQuestions.Add(Questions.simple(
            "The anterior limb of the internal capsule separates the globus pallidus from this structure.",
            "Caudate",
            Difficulty.Clinical));

        allQuestions.Add(Questions.box(
            "Identify four major structures that belong to the limbic system.",
            new string[] {"Parahippocampus", "Cingulate Gyrus", "Amygdala", "Hippocampal Formation" }));

        allQuestions.Add(Questions.box(
            "Which structures are involved in emotional or affective behavior?",
            new string[] {"Parahippocampus", "Amygdala", "Hippocampal Formation" }));

        allQuestions.Add(Questions.box(
            "These two structures make up the striatum.",
            new string[] {"Caudate", "Putamen"}));

        allQuestions.Add(Questions.region(
            "Point to the right pyramid.",
            "Brain Stem.CR",
            new Vector3(0.00694f, 0.46012f, 0.21660f),
            .15f,
            Difficulty.Easy));

        allQuestions.Add(Questions.region(
           "Point to the median sulcus.",
           "Brain Stem.CR",
           new Vector3(0.04132f, 0.59612f, 0.26163f),
           .1f,
            Difficulty.Clinical));

        allQuestions.Add(Questions.region(
            "Point to the left interventricular foramen of Monro.",
            "Ventricle",
            new Vector3(0.00752f, 1.08974f, -0.04794f),
            .1f));

        allQuestions.Add(Questions.region(
            "Point to the cerebral aqueduct.",
            "Ventricle",
            new Vector3(0.02703f, 0.90636f, 0.22083f),
            .08f));

        //What is Colliculi in hierarchy?? //Brain Stem Object..will need to be specified?
        allQuestions.Add(Questions.region(
            "Point to the region on the right that receives input from from the retina and visual cortex and is responsible for visual orientation.",
            "Brain Stem.CR", "super colliculi",
            new Vector3(-0.02829f, 0.97048f, 0.22802f),
            .125f));

        allQuestions.Add(Questions.region(
            "Point to the region on the right that receives input from the cochlear nuclei and is responsible for auditory orientation.",
            "Brain Stem.CR", "super colliculi",
            new Vector3(-0.04131f, 0.90669f, 0.24229f),
            0.125f));

        //Where is Thalamus LGN in hierarchy? specified later on
        allQuestions.Add(Questions.region(
            "Point to the region that receives DIRECT input from left ipsilateral and right contralateral retinal photoreceptors.",
            "Thalamus", "Thalamus (LGN)",
            new Vector3(0.21308f, 0.93959f, 0.14500f),
            .1f));

        //needs to be specified..later..MGN
        allQuestions.Add(Questions.region(
            "Point to the region that receives DIRECT input from the left inferior colliculi.",
            "Thalamus", "Thalamus (MGN)",
            new Vector3(0.16178f, 0.98193f, 0.20606f),
            .1f,
            Difficulty.Clinical));

        //Where in hierarchy
        allQuestions.Add(Questions.region(
            "Point to the corpora quadrigemina.",
            "Brain Stem.CR", "Colliculi",
            new Vector3(0.00711f, 0.93122f, 0.22916f),
            .2f,
            Difficulty.Easy));

        allQuestions.Add(Questions.region(
            "Point to the left anterior horn of the lateral ventricles.",
            "Ventricle",
            new Vector3(0.04556f, 1.18121f, -0.22127f),
            .2f,
            Difficulty.Easy));

        allQuestions.Add(Questions.region(
            "Point to the left posterior horn of the lateral ventricles.",
            "Ventricle",
            new Vector3(0.22887f, 0.97295f, 0.48792f),
            .2f,
            Difficulty.Easy));

        allQuestions.Add(Questions.region(
            "Point to the left inferior horn of the lateral ventricles.",
            "Ventricle",
            new Vector3(0.21206f, 0.93356f, 0.05560f),
            .2f,
            Difficulty.Easy));

        allQuestions.Add(Questions.region(
            "Point to the left trigone or atrium.",
            "Ventricle",
            new Vector3(0.22548f, 1.05177f, 0.34052f),
            .3f,
            Difficulty.Easy));

        allQuestions.Add(Questions.region(
            "Point to the structure that joins the third and fourth ventricles.",
            "Ventricle", "The cerebral aquaduct is circled.",
            new Vector3(0.02703f, 0.90636f, 0.22083f),
            .125f));

        allQuestions.Add(Questions.region(
            "Point to the foramen of Magendi.",
            "Ventricle", "The foramen of Magendi is circled.",
            new Vector3(0.04212f, 0.55442f, 0.28349f),
            .05f,
            Difficulty.Clinical));

        allQuestions.Add(Questions.region(
            "Point to the right foramen of Luschka.",
            "Ventricle", "The foramen of Luschka is circled.",
            new Vector3(-0.03725f, 0.65008f, 0.29867f),
            .05f,
            Difficulty.Clinical));

        //omitting this one because it should actually be on the spinal cord
        //allQuestions.Add(Questions.region(
        //    "Point to the central canal.",
        //    "Ventricle",
        //    new Vector3(0.04212f, 0.55442f, 0.28349f),
        //    .1f));

        //Where in hierarchy, Cerebral Aqueduct
        allQuestions.Add(Questions.region(
            "Hydrocephaly with enlargement of lateral and third ventricles suggest an obstruction in what structure?",
            "Ventricle", "The cerebral aqueduct is circled.",
            new Vector3(0.01839f, 0.93106f, 0.22613f),
            .05f));

        allQuestions.Add(Questions.region(
            "Hydrocephaly with enlargement only to the lateral ventricles suggests an obstruction in what structure? (choose the left one.)",
            "Ventricle", "The left interventricular foramen of Monro.",
            new Vector3(0.02677f, 1.10853f, -0.06865f),
            .07f,
            Difficulty.Clinical));

        allQuestions.Add(Questions.region(
           "Sulci limitans flank what structure?",
           "Brain Stem.CR", "The median sulcus.",
           new Vector3(0.03890f, 0.53653f, 0.32504f),
           .08f,
            Difficulty.Clinical));

        allQuestions.Add(Questions.simple(
            "Fibers from the hippocampus project to the hypothalamus and this region.",
            "Septal Nuclei",
            Difficulty.Clinical));

        allQuestions.Add(Questions.simple(
            "Fibers from the hippocampus project to the septal nuclei and this region.",
            "Hypothalamus",
            Difficulty.Clinical));

        allQuestions.Add(Questions.simple(
            "This branch of the hippocampal projection leads to the septal nuclei.",
            "Pre-commissural fornix branch",
            Difficulty.Clinical));

        allQuestions.Add(Questions.simple(
            "This branch of the hippocampal projection leads to the mammillary bodies.",
            "Post-commissural fornix branch",
            Difficulty.Clinical));

        allQuestions.Add(Questions.simple(
            "This structure lies dorsal to the corpus callosum and is associated with motivation and emotional behavior.",
            "Cingulate Gyrus"));

        allQuestions.Add(Questions.simple(
            "This cortex controls muscles to the face, lips, and tongue.",
            new string[] {"Frontal Lobe.CR", "Frontal Lobe.CL"},
            Difficulty.Easy));

        allQuestions.Add(Questions.simple(
            "Damage to this cortical region can produce deficits in eye movements and behavior that is flippant, impulsive, and lacking in social decorum.",
            new string[] { "Frontal Lobe.CR", "Frontal Lobe.CL" }));

        allQuestions.Add(Questions.simple(
            "This cortical region is important for forming mental images of how the outside world is spatially organized and where your body is located relative to the world.",
            new string[] { "Parietal Lobe.CR", "Parietal Lobe.CL" }));

        //Not sure of the naming of this object...please correct if needed
        allQuestions.Add(Questions.simple(
            "This cortical region contains the calcarine cortex.",
            new string[] { "Occipital Lobe.CR", "Occipital Lobe.CL" }));

        allQuestions.Add(Questions.simple(
            "This cortical region receives gustatory and visceral sensory inputs.",
            "Insula"));

        allQuestions.Add(Questions.simple(
            "Which structure provides a cushioning function for the brain.",
            "Ventricle"));

        allQuestions.Add(Questions.simple(
            "This fiber pathway provides a route for olfactory information to cross from one side of the brain to the other.",
            "Anterior Commissure"));

        allQuestions.Add(Questions.simple(
            "This region sits between the external capsule and extreme capsule.",
            "Claustrum",
            Difficulty.Clinical));

        allQuestions.Add(Questions.simple(
            "Lateral to the extreme capsule is this region.",
            "Insula"));

        allQuestions.Add(Questions.simple(
            "What structure is flanked by the extreme capsule and the insula?",
            "Claustrum",
            Difficulty.Clinical));

        allQuestions.Add(Questions.simple(
            "What structure is divided by the medial medullary lamina?",
            "Globus Pallidus",
            Difficulty.Clinical));

        allQuestions.Add(Questions.simple(
            "This brain region extends anteriorly from the lamina terminalis and posteriorly to the mammillary bodies.",
            "Hypothalamus"));

        allQuestions.Add(Questions.simple(
            "This fiber bundle connects the hippocampus with the septum and mammillary bodies.",
            "Fornix Body"));

        allQuestions.Add(Questions.simple(
            "The efferent half of this pathway is called the fimbria.",
            "Fornix Body"));

        allQuestions.Add(Questions.simple(
            "The dorsolateral wall of the third ventricle is formed by this structure.",
            "Thalamus",
            Difficulty.Clinical));

        allQuestions.Add(Questions.simple(
            "The ventrolateral walls of the third ventricle is formed by this brain region.",
            "Hypothalamus",
            Difficulty.Clinical));

        allQuestions.Add(Questions.simple(
            "This structure can be found in the frontal, parietal, and temporal lobes.",
            "Caudate",
            Difficulty.Clinical));

        allQuestions.Add(Questions.simple(
            "This region is vulnerable to herniation at the falx cerebri due to lateral forces on the brain.",
            "Cingulate Gyrus",
            Difficulty.Clinical));

        allQuestions.Add(Questions.simple(
            "This region is vulnerable to herniation at the foramen magnum due to downward forces on the brain.",
            new string[] {"Cerebellum.CL", "Cerebellum.CR" },
            Difficulty.Clinical));

        allQuestions.Add(Questions.box(
            "These two structures are separated by the lateral medullary lamina.",
            new string[] { "Globus Pallidus", "Putamen" },
            Difficulty.Clinical));

        allQuestions.Add(Questions.box(
            "These two structures are separated by the anterior limb of the internal capsule.",
            new string[] {"Caudate", "Putamen"},
            Difficulty.Clinical));

        allQuestions.Add(Questions.box(
            "These two structures are separated by the posterior limb of the internal capsule.",
            new string[] {"Globus Pallidus", "Thalamus"},
            Difficulty.Clinical));

        allQuestions.Add(Questions.box(
            "What structures lie between the internal capsule and the external capsule?",
            new string[] {"Globus Pallidus", "Putamen"},
            Difficulty.Clinical));

        allQuestions.Add(Questions.box(
            "What structures comprise the basal ganglia?",
            new string[] {"Globus Pallidus","Putamen", "Caudate", "Nucleus Accumbens"},
            Difficulty.Clinical));

        allQuestions.Add(Questions.box(
            "Parkinson's disease results from death of dopamine neurons in the substantia nigra that project to these two regions.",
            new string[] {"Caudate", "Putamen"},
            Difficulty.Clinical));

        allQuestions.Add(Questions.box(
            "What two brain regions make up the lenticular nucleus?",
            new string[] {"Putamen", "Globus Pallidus" }));

        //this must be the last part
        //Debug.Log("Generated " + allQuestions.Count.ToString() + " questions.");
        return allQuestions;
    }
}
