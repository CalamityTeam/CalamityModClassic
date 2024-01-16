using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items {
public class GallantPickaxe : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/GallantPickaxe");
        return true;
    }
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Gallant Pickaxe");
        Item.damage = 55;
        Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
        Item.width = 58;
        Item.height = 58;
        ////Tooltip.SetDefault("Mining with nuclear power!");
        Item.useTime = 1;
        Item.useAnimation = 12;
        Item.useTurn = true;
        Item.pick = 250;
        Item.useStyle = 1;
        Item.knockBack = 6;
        Item.value = 225000;
        Item.rare = 9;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.tileBoost += 6;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "MeldiateBar", 9);
        recipe.AddIngredient(null, "GalacticaSingularity");
        recipe.AddRecipeGroup("LunarPickaxe");
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if(Main.rand.Next(5) == 0)
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 58);
        }
    }
    
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
		target.AddBuff(BuffID.CursedInferno, 500);
	}
}}