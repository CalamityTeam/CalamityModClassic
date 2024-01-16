using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items {
public class BlossomPickaxe : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/BlossomPickaxe");
        return true;
    }
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Blossom Pickaxe");
        Item.damage = 40;
        Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
        Item.width = 56;
        Item.height = 60;
        Item.useTime = 6;
        Item.useAnimation = 15;
        Item.useTurn = true;
        Item.pick = 220;
        Item.useStyle = 1;
        Item.knockBack = 4.5f;
        Item.value = 207000;
        Item.rare = 8;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.tileBoost += 4;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "UeliaceBar", 7);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if(Main.rand.Next(5) == 0)
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 75);
        }
    }
    
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
		target.AddBuff(BuffID.OnFire, 200);
		target.AddBuff(BuffID.CursedInferno, 200);
	}
}}