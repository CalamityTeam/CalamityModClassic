using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items {
public class FlamebeakHampick : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/FlamebeakHampick");
        return true;
    }
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Flamebeak Hampick");
        Item.damage = 38;
        Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
        Item.width = 36;
        Item.height = 36;
        Item.useTime = 15;
        Item.useAnimation = 15;
        Item.useTurn = true;
        Item.pick = 205;
        Item.hammer = 120;
        Item.useStyle = 1;
        Item.knockBack = 3.5f;
        Item.value = 197000;
        Item.rare = 7;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.tileBoost += 2;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "CruptixBar", 7);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }

    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if(Main.rand.Next(5) == 0)
        {
        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 127);
        }
    }
    
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
		target.AddBuff(BuffID.OnFire, 300);
	}
}}