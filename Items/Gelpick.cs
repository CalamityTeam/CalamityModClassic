using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items {
public class Gelpick : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Gelpick");
        return true;
    }
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Gelpick");
        Item.damage = 20;
        Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
        Item.width = 38;
        Item.height = 38;
        Item.useTime = 15;
        Item.useAnimation = 15;
        Item.pick = 100;
        Item.useStyle = 1;
        Item.knockBack = 4;
        Item.value = 107000;
        Item.rare = 4;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "PurifiedGel", 15);
        recipe.AddIngredient(ItemID.Gel, 30);
        recipe.AddIngredient(ItemID.HellstoneBar, 5);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if(Main.rand.Next(4) == 0)
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 20);
        }
    }
    
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
		target.AddBuff(BuffID.Slimed, 100);
	}
}}