﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TajikSpeechRecognition.Core
{
    public class ConfigTemplates
    {
        public static string GetSphinxTrainCongig(string modelName = "tajik")
        {
            var builder = new StringBuilder();
            builder.AppendLine("# Configuration script for sphinx trainer                  -*-mode:Perl-*-");
            builder.AppendLine("");
            builder.AppendLine("$CFG_VERBOSE = 1;# Determines how much goes to the screen.");
            builder.AppendLine("");
            builder.AppendLine("# These are filled in at configuration time");
            builder.AppendLine($"$CFG_DB_NAME = \"{modelName}\";");
            builder.AppendLine("# Experiment name, will be used to name model files and log files");
            builder.AppendLine("$CFG_EXPTNAME = \"$CFG_DB_NAME\";");
            builder.AppendLine("");
            builder.AppendLine("# Directory containing SphinxTrain binaries");
            builder.AppendLine($"$CFG_BASE_DIR = \"{AppManager.OutputDir}/{modelName}\";");
            builder.AppendLine($"$CFG_SPHINXTRAIN_DIR = \"{AppManager.SphinxDir}/sphinxtrain\";");
            builder.AppendLine($"$CFG_BIN_DIR = \"{AppManager.SphinxDir}/sphinxtrain/bin/Release/Win32\";");
            builder.AppendLine($"$CFG_SCRIPT_DIR = \"{AppManager.SphinxDir}/sphinxtrain/scripts\";");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("# Audio waveform and feature file information");
            builder.AppendLine("$CFG_WAVFILES_DIR = \"$CFG_BASE_DIR/wav\";");
            builder.AppendLine("$CFG_WAVFILE_EXTENSION = 'wav';");
            builder.AppendLine("$CFG_WAVFILE_TYPE = 'mswav'; # one of nist, mswav, raw");
            builder.AppendLine("$CFG_FEATFILES_DIR = \"$CFG_BASE_DIR/feat\";");
            builder.AppendLine("$CFG_FEATFILE_EXTENSION = 'mfc';");
            builder.AppendLine("");
            builder.AppendLine("# Feature extraction parameters");
            builder.AppendLine("$CFG_WAVFILE_SRATE = 16000.0;");
            builder.AppendLine("$CFG_NUM_FILT = 25; # For wideband speech it's 25, for telephone 8khz reasonable value is 15");
            builder.AppendLine("$CFG_LO_FILT = 130; # For telephone 8kHz speech value is 200");
            builder.AppendLine("$CFG_HI_FILT = 6800; # For telephone 8kHz speech value is 3500");
            builder.AppendLine("$CFG_TRANSFORM = \"dct\"; # Previously legacy transform is used, but dct is more accurate");
            builder.AppendLine("$CFG_LIFTER = \"22\"; # Cepstrum lifter is smoothing to improve recognition");
            builder.AppendLine("$CFG_VECTOR_LENGTH = 13; # 13 is usually enough");
            builder.AppendLine("");
            builder.AppendLine("$CFG_MIN_ITERATIONS = 1;  # BW Iterate at least this many times");
            builder.AppendLine("$CFG_MAX_ITERATIONS = 10; # BW Don't iterate more than this, somethings likely wrong.");
            builder.AppendLine("");
            builder.AppendLine("# (none/max) Type of AGC to apply to input files");
            builder.AppendLine("$CFG_AGC = 'none';");
            builder.AppendLine("# (current/none) Type of cepstral mean subtraction/normalization");
            builder.AppendLine("# to apply to input files");
            builder.AppendLine("$CFG_CMN = 'batch';");
            builder.AppendLine("# (yes/no) Normalize variance of input files to 1.0");
            builder.AppendLine("$CFG_VARNORM = 'no';");
            builder.AppendLine("# (yes/no) Train full covariance matrices");
            builder.AppendLine("$CFG_FULLVAR = 'no';");
            builder.AppendLine("# (yes/no) Use diagonals only of full covariance matrices for");
            builder.AppendLine("# Forward-Backward evaluation (recommended if CFG_FULLVAR is yes)");
            builder.AppendLine("$CFG_DIAGFULL = 'no';");
            builder.AppendLine("");
            builder.AppendLine("# (yes/no) Perform vocal tract length normalization in training.  This");
            builder.AppendLine("# will result in a \"normalized\" model which requires VTLN to be done");
            builder.AppendLine("# during decoding as well.");
            builder.AppendLine("$CFG_VTLN = 'no';");
            builder.AppendLine("# Starting warp factor for VTLN");
            builder.AppendLine("$CFG_VTLN_START = 0.80;");
            builder.AppendLine("# Ending warp factor for VTLN");
            builder.AppendLine("$CFG_VTLN_END = 1.40;");
            builder.AppendLine("# Step size of warping factors");
            builder.AppendLine("$CFG_VTLN_STEP = 0.05;");
            builder.AppendLine("");
            builder.AppendLine("# Directory to write queue manager logs to");
            builder.AppendLine("$CFG_QMGR_DIR = \"$CFG_BASE_DIR/qmanager\";");
            builder.AppendLine("# Directory to write training logs to");
            builder.AppendLine("$CFG_LOG_DIR = \"$CFG_BASE_DIR/logdir\";");
            builder.AppendLine("# Directory for re-estimation counts");
            builder.AppendLine("$CFG_BWACCUM_DIR = \"$CFG_BASE_DIR/bwaccumdir\";");
            builder.AppendLine("# Directory to write model parameter files to");
            builder.AppendLine("$CFG_MODEL_DIR = \"$CFG_BASE_DIR/model_parameters\";");
            builder.AppendLine("");
            builder.AppendLine("# Directory containing transcripts and control files for");
            builder.AppendLine("# speaker-adaptive training");
            builder.AppendLine("$CFG_LIST_DIR = \"$CFG_BASE_DIR/etc\";");
            builder.AppendLine("");
            builder.AppendLine("# Decoding variables for MMIE training");
            builder.AppendLine("$CFG_LANGUAGEWEIGHT = \"11.5\";");
            builder.AppendLine("$CFG_BEAMWIDTH      = \"1e-100\";");
            builder.AppendLine("$CFG_WORDBEAM       = \"1e-80\";");
            builder.AppendLine("$CFG_LANGUAGEMODEL  = \"$CFG_LIST_DIR/$CFG_DB_NAME.lm.DMP\";");
            builder.AppendLine("$CFG_WORDPENALTY    = \"0.2\";");
            builder.AppendLine("");
            builder.AppendLine("# Lattice pruning variables");
            builder.AppendLine("$CFG_ABEAM              = \"1e-50\";");
            builder.AppendLine("$CFG_NBEAM              = \"1e-10\";");
            builder.AppendLine("$CFG_PRUNED_DENLAT_DIR  = \"$CFG_BASE_DIR/pruned_denlat\";");
            builder.AppendLine("");
            builder.AppendLine("# MMIE training related variables");
            builder.AppendLine("$CFG_MMIE = \"no\";");
            builder.AppendLine("$CFG_MMIE_MAX_ITERATIONS = 5;");
            builder.AppendLine("$CFG_LATTICE_DIR = \"$CFG_BASE_DIR/lattice\";");
            builder.AppendLine("$CFG_MMIE_TYPE   = \"rand\"; # Valid values are \"rand\", \"best\" or \"ci\"");
            builder.AppendLine("$CFG_MMIE_CONSTE = \"3.0\";");
            builder.AppendLine("$CFG_NUMLAT_DIR  = \"$CFG_BASE_DIR/numlat\";");
            builder.AppendLine("$CFG_DENLAT_DIR  = \"$CFG_BASE_DIR/denlat\";");
            builder.AppendLine("");
            builder.AppendLine("# Variables used in main training of models");
            builder.AppendLine("$CFG_DICTIONARY     = \"$CFG_LIST_DIR/$CFG_DB_NAME.dic\";");
            builder.AppendLine("$CFG_RAWPHONEFILE   = \"$CFG_LIST_DIR/$CFG_DB_NAME.phone\";");
            builder.AppendLine("$CFG_FILLERDICT     = \"$CFG_LIST_DIR/$CFG_DB_NAME.filler\";");
            builder.AppendLine("$CFG_LISTOFFILES    = \"$CFG_LIST_DIR/${CFG_DB_NAME}_train.fileids\";");
            builder.AppendLine("$CFG_TRANSCRIPTFILE = \"$CFG_LIST_DIR/${CFG_DB_NAME}_train.transcription\";");
            builder.AppendLine("$CFG_FEATPARAMS     = \"$CFG_LIST_DIR/feat.params\";");
            builder.AppendLine("");
            builder.AppendLine("# Variables used in characterizing models");
            builder.AppendLine("");
            builder.AppendLine("$CFG_HMM_TYPE = '.cont.'; # Sphinx 4, PocketSphinx");
            builder.AppendLine("#$CFG_HMM_TYPE  = '.semi.'; # PocketSphinx");
            builder.AppendLine("#$CFG_HMM_TYPE  = '.ptm.'; # PocketSphinx (larger data sets)");
            builder.AppendLine("");
            builder.AppendLine("if (($CFG_HMM_TYPE ne \".semi.\")");
            builder.AppendLine("    and ($CFG_HMM_TYPE ne \".ptm.\")");
            builder.AppendLine("    and ($CFG_HMM_TYPE ne \".cont.\")) {");
            builder.AppendLine("  die \"Please choose one CFG_HMM_TYPE out of '.cont.', '.ptm.', or '.semi.', \" .");
            builder.AppendLine("    \"currently $CFG_HMM_TYPE\\n\";");
            builder.AppendLine("}");
            builder.AppendLine("");
            builder.AppendLine("# This configuration is fastest and best for most acoustic models in");
            builder.AppendLine("# PocketSphinx and Sphinx-III.  See below for Sphinx-II.");
            builder.AppendLine("$CFG_STATESPERHMM = 3;");
            builder.AppendLine("$CFG_SKIPSTATE = 'no';");
            builder.AppendLine("");
            builder.AppendLine("if ($CFG_HMM_TYPE eq '.semi.') {");
            builder.AppendLine("  $CFG_DIRLABEL = 'semi';");
            builder.AppendLine("# Four stream features for PocketSphinx");
            builder.AppendLine("  $CFG_FEATURE = \"s2_4x\";");
            builder.AppendLine("  $CFG_NUM_STREAMS = 4;");
            builder.AppendLine("  $CFG_INITIAL_NUM_DENSITIES = 256;");
            builder.AppendLine("  $CFG_FINAL_NUM_DENSITIES = 256;");
            builder.AppendLine("  die \"For semi continuous models, the initial and final models have the same density\" ");
            builder.AppendLine("    if ($CFG_INITIAL_NUM_DENSITIES != $CFG_FINAL_NUM_DENSITIES);");
            builder.AppendLine("} elsif ($CFG_HMM_TYPE eq '.ptm.') {");
            builder.AppendLine("  $CFG_DIRLABEL = 'ptm';");
            builder.AppendLine("# Four stream features for PocketSphinx");
            builder.AppendLine("  $CFG_FEATURE = \"s2_4x\";");
            builder.AppendLine("  $CFG_NUM_STREAMS = 4;");
            builder.AppendLine("  $CFG_INITIAL_NUM_DENSITIES = 64;");
            builder.AppendLine("  $CFG_FINAL_NUM_DENSITIES = 64;");
            builder.AppendLine("  die \"For phonetically tied models, the initial and final models have the same density\" ");
            builder.AppendLine("    if ($CFG_INITIAL_NUM_DENSITIES != $CFG_FINAL_NUM_DENSITIES);");
            builder.AppendLine("} elsif ($CFG_HMM_TYPE eq '.cont.') {");
            builder.AppendLine("  $CFG_DIRLABEL = 'cont';");
            builder.AppendLine("# Single stream features - Sphinx 3");
            builder.AppendLine("  $CFG_FEATURE = \"1s_c_d_dd\";");
            builder.AppendLine("  $CFG_NUM_STREAMS = 1;");
            builder.AppendLine("  $CFG_INITIAL_NUM_DENSITIES = 1;");
            builder.AppendLine("  $CFG_FINAL_NUM_DENSITIES = 8;");
            builder.AppendLine("  die \"The initial has to be less than the final number of densities\" ");
            builder.AppendLine("    if ($CFG_INITIAL_NUM_DENSITIES > $CFG_FINAL_NUM_DENSITIES);");
            builder.AppendLine("}");
            builder.AppendLine("");
            builder.AppendLine("# Number of top gaussians to score a frame. A little bit less accurate computations");
            builder.AppendLine("# make training significantly faster. Uncomment to apply this during the training");
            builder.AppendLine("# For good accuracy make sure you are using the same setting in decoder");
            builder.AppendLine("# In theory this can be different for various training stages. For example 4 for");
            builder.AppendLine("# CI stage and 16 for CD stage");
            builder.AppendLine("# $CFG_CI_TOPN = 4;");
            builder.AppendLine("# $CFG_CD_TOPN = 16;");
            builder.AppendLine("");
            builder.AppendLine("# (yes/no) Train multiple-gaussian context-independent models (useful");
            builder.AppendLine("# for alignment, use 'no' otherwise) in the models created");
            builder.AppendLine("# specifically for forced alignment");
            builder.AppendLine("$CFG_FALIGN_CI_MGAU = 'no';");
            builder.AppendLine("# (yes/no) Train multiple-gaussian context-independent models (useful");
            builder.AppendLine("# for alignment, use 'no' otherwise)");
            builder.AppendLine("$CFG_CI_MGAU = 'no';");
            builder.AppendLine("# (yes/no) Train context-dependent models");
            builder.AppendLine("$CFG_CD_TRAIN = 'no';");
            builder.AppendLine("# Number of tied states (senones) to create in decision-tree clustering");
            builder.AppendLine("$CFG_N_TIED_STATES = 200;");
            builder.AppendLine("# How many parts to run Forward-Backward estimatinon in");
            builder.AppendLine("$CFG_NPART = 1;");
            builder.AppendLine("");
            builder.AppendLine("# (yes/no) Train a single decision tree for all phones (actually one");
            builder.AppendLine("# per state) (useful for grapheme-based models, use 'no' otherwise)");
            builder.AppendLine("$CFG_CROSS_PHONE_TREES = 'no';");
            builder.AppendLine("");
            builder.AppendLine("# Use force-aligned transcripts (if available) as input to training");
            builder.AppendLine("$CFG_FORCEDALIGN = 'no';");
            builder.AppendLine("");
            builder.AppendLine("# Use a specific set of models for force alignment.  If not defined,");
            builder.AppendLine("# context-independent models for the current experiment will be used.");
            builder.AppendLine("$CFG_FORCE_ALIGN_MODELDIR = \"$CFG_MODEL_DIR/$CFG_EXPTNAME.falign_ci_$CFG_DIRLABEL\";");
            builder.AppendLine("");
            builder.AppendLine("# Use a specific dictionary and filler dictionary for force alignment.");
            builder.AppendLine("# If these are not defined, a dictionary and filler dictionary will be");
            builder.AppendLine("# created from $CFG_DICTIONARY and $CFG_FILLERDICT, with noise words");
            builder.AppendLine("# removed from the filler dictionary and added to the dictionary (this");
            builder.AppendLine("# is because the force alignment is not very good at inserting them)");
            builder.AppendLine("");
            builder.AppendLine("# $CFG_FORCE_ALIGN_DICTIONARY = \"$ST::CFG_BASE_DIR/falignout$ST::CFG_EXPTNAME.falign.dict\";;");
            builder.AppendLine("# $CFG_FORCE_ALIGN_FILLERDICT = \"$ST::CFG_BASE_DIR/falignout/$ST::CFG_EXPTNAME.falign.fdict\";;");
            builder.AppendLine("");
            builder.AppendLine("# Use a particular beam width for force alignment.  The wider");
            builder.AppendLine("# (i.e. smaller numerically) the beam, the fewer sentences will be");
            builder.AppendLine("# rejected for bad alignment.");
            builder.AppendLine("$CFG_FORCE_ALIGN_BEAM = 1e-60;");
            builder.AppendLine("");
            builder.AppendLine("# Calculate an LDA/MLLT transform?");
            builder.AppendLine("$CFG_LDA_MLLT = 'no';");
            builder.AppendLine("# Dimensionality of LDA/MLLT output");
            builder.AppendLine("$CFG_LDA_DIMENSION = 29;");
            builder.AppendLine("");
            builder.AppendLine("# This is actually just a difference in log space (it doesn't make");
            builder.AppendLine("# sense otherwise, because different feature parameters have very");
            builder.AppendLine("# different likelihoods)");
            builder.AppendLine("$CFG_CONVERGENCE_RATIO = 0.1;");
            builder.AppendLine("");
            builder.AppendLine("# Queue::POSIX for multiple CPUs on a local machine");
            builder.AppendLine("# Queue::PBS to use a PBS/TORQUE queue");
            builder.AppendLine("$CFG_QUEUE_TYPE = \"Queue\";");
            builder.AppendLine("");
            builder.AppendLine("# Name of queue to use for PBS/TORQUE");
            builder.AppendLine("$CFG_QUEUE_NAME = \"workq\";");
            builder.AppendLine("");
            builder.AppendLine("# (yes/no) Build questions for decision tree clustering automatically");
            builder.AppendLine("$CFG_MAKE_QUESTS = \"yes\";");
            builder.AppendLine("# If CFG_MAKE_QUESTS is yes, questions are written to this file.");
            builder.AppendLine("# If CFG_MAKE_QUESTS is no, questions are read from this file.");
            builder.AppendLine("$CFG_QUESTION_SET = \"${CFG_BASE_DIR}/model_architecture/${CFG_EXPTNAME}.tree_questions\";");
            builder.AppendLine("#$CFG_QUESTION_SET = \"${CFG_BASE_DIR}/linguistic_questions\";");
            builder.AppendLine("");
            builder.AppendLine("$CFG_CP_OPERATION = \"${CFG_BASE_DIR}/model_architecture/${CFG_EXPTNAME}.cpmeanvar\";");
            builder.AppendLine("");
            builder.AppendLine("# Configuration for grapheme-to-phoneme model");
            builder.AppendLine("$CFG_G2P_MODEL= 'no';");
            builder.AppendLine("");
            builder.AppendLine("# Configuration script for sphinx decoder ");
            builder.AppendLine("");
            builder.AppendLine("# Variables starting with $DEC_CFG_ refer to decoder specific");
            builder.AppendLine("# arguments, those starting with $CFG_ refer to trainer arguments,");
            builder.AppendLine("# some of them also used by the decoder.");
            builder.AppendLine("");
            builder.AppendLine("$DEC_CFG_VERBOSE = 1;		# Determines how much goes to the screen.");
            builder.AppendLine("");
            builder.AppendLine("# These are filled in at configuration time");
            builder.AppendLine("");
            builder.AppendLine("# Name of the decoding script to use (psdecode.pl or s3decode.pl, probably)");
            builder.AppendLine("$DEC_CFG_SCRIPT = 'psdecode.pl';");
            builder.AppendLine("");
            builder.AppendLine("$DEC_CFG_EXPTNAME = \"$CFG_EXPTNAME\";");
            builder.AppendLine("$DEC_CFG_JOBNAME  = \"$CFG_EXPTNAME\".\"_job\";");
            builder.AppendLine("");
            builder.AppendLine("# Models to use.");
            builder.AppendLine("if ($CFG_CD_TRAIN eq 'yes') {");
            builder.AppendLine("  $DEC_CFG_MODEL_NAME = \"$CFG_EXPTNAME.cd_${ CFG_DIRLABEL}_${ CFG_N_TIED_STATES}\";");
            builder.AppendLine("} else {");
            builder.AppendLine("  $DEC_CFG_MODEL_NAME = \"$CFG_EXPTNAME.ci_${ CFG_DIRLABEL}\";");
            builder.AppendLine("}");
            builder.AppendLine("");
            builder.AppendLine("$DEC_CFG_FEATFILES_DIR = \"$CFG_BASE_DIR/feat\";");
            builder.AppendLine("$DEC_CFG_FEATFILE_EXTENSION = '.mfc';");
            builder.AppendLine("$DEC_CFG_AGC = $CFG_AGC;");
            builder.AppendLine("$DEC_CFG_CMN = $CFG_CMN;");
            builder.AppendLine("$DEC_CFG_VARNORM = $CFG_VARNORM;");
            builder.AppendLine("");
            builder.AppendLine("$DEC_CFG_QMGR_DIR = \"$CFG_BASE_DIR/qmanager\";");
            builder.AppendLine("$DEC_CFG_LOG_DIR = \"$CFG_BASE_DIR/logdir\";");
            builder.AppendLine("$DEC_CFG_MODEL_DIR = \"$CFG_MODEL_DIR\";");
            builder.AppendLine("");
            builder.AppendLine("$DEC_CFG_DICTIONARY     = \"$CFG_BASE_DIR/etc/$CFG_DB_NAME.dic\";");
            builder.AppendLine("$DEC_CFG_FILLERDICT     = \"$CFG_BASE_DIR/etc/$CFG_DB_NAME.filler\";");
            builder.AppendLine("$DEC_CFG_LISTOFFILES    = \"$CFG_BASE_DIR/etc/${CFG_DB_NAME}_test.fileids\";");
            builder.AppendLine("$DEC_CFG_TRANSCRIPTFILE = \"$CFG_BASE_DIR/etc/${CFG_DB_NAME}_test.transcription\";");
            builder.AppendLine("$DEC_CFG_RESULT_DIR     = \"$CFG_BASE_DIR/result\";");
            builder.AppendLine("$DEC_CFG_PRESULT_DIR     = \"$CFG_BASE_DIR/presult\";");
            builder.AppendLine("");
            builder.AppendLine("# This variables, used by the decoder, have to be user defined, and");
            builder.AppendLine("# may affect the decoder output");
            builder.AppendLine("");
            builder.AppendLine("");
            builder.AppendLine("$DEC_CFG_LANGUAGEMODEL  = \"$CFG_BASE_DIR/etc/${CFG_DB_NAME}.lm.DMP\";");
            builder.AppendLine("# Or can be JSGF or FSG too, used if uncommented");
            builder.AppendLine("# $DEC_CFG_GRAMMAR  = \"$CFG_BASE_DIR/etc/${CFG_DB_NAME}.jsgf\";");
            builder.AppendLine("# $DEC_CFG_FSG  = \"$CFG_BASE_DIR/etc/${CFG_DB_NAME}.fsg\";");
            builder.AppendLine("");
            builder.AppendLine("$DEC_CFG_LANGUAGEWEIGHT = \"10\";");
            builder.AppendLine("$DEC_CFG_BEAMWIDTH = \"1e-100\";");
            builder.AppendLine("$DEC_CFG_WORDBEAM = \"1e-60\";");
            builder.AppendLine("$DEC_CFG_WORDPENALTY = \"0.2\";");
            builder.AppendLine("");
            builder.AppendLine("$DEC_CFG_ALIGN = \"builtin\";");
            builder.AppendLine("");
            builder.AppendLine("$DEC_CFG_NPART = 1;		#  Define how many pieces to split decode in");
            builder.AppendLine("");
            builder.AppendLine("# This variable has to be defined, otherwise utils.pl will not load.");
            builder.AppendLine("$CFG_DONE = 1;");
            builder.AppendLine("");
            builder.AppendLine("return 1;");

            return builder.ToString();
        }

        public static string GetFeatParams()
        {
            var builder = new StringBuilder();
            builder.AppendLine("-lowerf __CFG_LO_FILT__");
            builder.AppendLine("-upperf __CFG_HI_FILT__");
            builder.AppendLine("-nfilt __CFG_NUM_FILT__");
            builder.AppendLine("-transform __CFG_TRANSFORM__");
            builder.AppendLine("-lifter __CFG_LIFTER__");
            builder.AppendLine("-feat __CFG_FEATURE__");
            builder.AppendLine("-svspec __CFG_SVSPEC__");
            builder.AppendLine("-agc __CFG_AGC__");
            builder.AppendLine("-cmn __CFG_CMN__");
            builder.AppendLine("-varnorm __CFG_VARNORM__");
            return builder.ToString();
        }

        public static string GetPhonemes(IEnumerable<string> usedPhonemes)
        {
            var builder = new StringBuilder();
            var phonemes = usedPhonemes.SelectMany(uf => uf.Trim().Split(' '));
            foreach (var phonem in phonemes)
                builder.AppendLine(phonem);

            builder.AppendLine("SIL");
            return builder.ToString();
        }
    }
}