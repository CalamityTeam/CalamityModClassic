using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items {
public class BeastialPickaxe : ModItem
{
	public override void SetStaticDefaults()
	{
 		//DisplayName.SetDefault("Beastial Pickaxe");
 	}
	
    public override void SetDefaults()
    {
        Item.damage = 35;
        Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
        Item.width = 46;
        Item.height = 46;
        Item.useTime = 7;
        Item.useAnimation = 15;
        Item.useTurn = true;
        Item.pick = 200;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 4.5f;
        Item.value = 177000;
        Item.rare = ItemRarityID.LightPurple;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "DraedonBar", 7);
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