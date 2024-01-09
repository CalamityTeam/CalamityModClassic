using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items {
public class BerserkerWaraxe : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Berserker Waraxe");
 	}
	
    public override void SetDefaults()
    {
        Item.damage = 51;
        Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
        Item.width = 52;
        Item.height = 48;
        Item.useTime = 27;
        Item.useAnimation = 27;
        Item.useTurn = true;
        Item.axe = 30;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 8;
        Item.value = 650000;
        Item.rare = ItemRarityID.LightPurple;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "DraedonBar", 9);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if(Main.rand.NextBool(5))
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.GreenTorch);
        }
    }
    
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
		target.AddBuff(BuffID.Venom, 200);
	}
}}